﻿using KAS.API.MIDDEWARE.Entity;
using KAS.ECOS.API.Code;
using KAS.ECOS.API.Entity;
using KAS.Entity.DB.ECOS;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{
    [ApiController]
    [Route("api/KAS/ECOS")]
    public partial class EcosController : Controller
    {
        private readonly KASECOSContext w;

        public EcosController(KASECOSContext context)
        {
            w = context;
        }

        /// <summary>
        /// Đăng nhập vào hệ thống
        /// </summary>
        /// <param name="ie"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<OutEntity> getLogin(InEntity ie)
        {
            try
            {
                var acc = ie.getData<AccountEntity>();


                var dbAcc = w.RolesUsers.FirstOrDefault(ii => ii.User==acc.us && !ii.IsDeleted);

                if (dbAcc!=null)  //Có tài khoản
                {
                    if (!string.IsNullOrEmpty(ie.Token) && (dbAcc.User != acc.us))
                    {

                        return new OutEntity("", "Token không hợp lệ");
                    }

                    var listFunction = w.KasProductsFunctionsPermissions.Count(ii => ii.CustomerId==dbAcc.CustomerId
                    && ii.KasProductId == ie.KASProductName
                    && ii.RoleName==dbAcc.RoleName && !ii.IsDeleted
                    && ii.Expired.HasValue ? DateTime.UtcNow<ii.Expired.Value : true
                    );

                    //token nếu là ecos thì xác thực qua telegram
                    if (listFunction>0 || dbAcc.RoleName=="sysadmin") //có tính năng đã phân quyền
                    {
                        if (!string.IsNullOrEmpty(dbAcc.Token))
                        {
                            if (DateTime.UtcNow>= dbAcc.TokenExpired) //Token hết hạn thì reset token
                            {
                                dbAcc.Token="";
                            }
                        }
                        if (string.IsNullOrEmpty(dbAcc.Token))//Nếu chưa có Token thì tạo Token
                        {
                            using (var dbContextTransaction = w.Database.BeginTransaction())
                            {
                                if (ie.KASProductName=="ecos")
                                {
                                    dbAcc.TokenExpired = DateTime.UtcNow.AddDays(1);
                                }
                                else
                                {
                                    dbAcc.TokenExpired = DateTime.UtcNow.AddYears(1);

                                }
                                dbAcc.Token = Core.CoreString.StringMD5(string.Join(ie.KASProductName, dbAcc.RoleName, dbAcc.TokenExpired.Value.ToOADate()));

                                await w.TokensLogs.AddAsync(new TokensLog()
                                {
                                    CustomerId =dbAcc.CustomerId,
                                    DeviceId =Newtonsoft.Json.JsonConvert.SerializeObject(ie.DeivceInfo),
                                    IsDeleted =false,
                                    KasProductsId = ie.KASProductName,
                                    RoleName = dbAcc.RoleName,
                                    User = dbAcc.User,
                                    Token =  dbAcc.Token,
                                    TokenCreate = DateTime.UtcNow,
                                    TokenExpired = dbAcc.TokenExpired,

                                });

                                await w.SaveChangesAsync();
                                await dbContextTransaction.CommitAsync();
                            }

                        }

                        return new OutEntity(new
                        {
                            Token = dbAcc.Token,
                            Expired = dbAcc.TokenExpired
                        }, "");   //Trả về token báo đăng nhập thành công

                    }
                }


                return new OutEntity("", "Token không hợp lệ");
            }
            catch
            {
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng đăng nhập lại sau");
            }
        }


        /// <summary>
        /// Kiểm tra token có hợp lệ hay không
        /// </summary>
        /// <param name="ie"></param>
        /// <returns></returns>
        [HttpPost("Token")]
        public OutEntity checkToken(InEntity ie)
        {
            try
            {
                // var acc = ie.getData<AccountEntity>();


                var dbAcc = w.RolesUsers.FirstOrDefault(ii => ii.Token==ie.Token && !ii.IsDeleted);

                if (dbAcc!=null)  //Có token
                {


                    if (!string.IsNullOrEmpty(dbAcc.Token))
                    {
                        if (DateTime.UtcNow>= dbAcc.TokenExpired) //Token hết hạn thì reset token
                        {
                            return new OutEntity("", "Token hết thời gian sử dụng");
                        }
                    }


                    var listFunction = w.KasProductsFunctionsPermissions.Count(ii => ii.CustomerId==dbAcc.CustomerId
                   && ii.KasProductId == ie.KASProductName
                   && ii.RoleName==dbAcc.RoleName && !ii.IsDeleted
                   && ii.Expired.HasValue ? DateTime.UtcNow<ii.Expired.Value : true
                   );
                    //token nếu là ecos thì xác thực qua telegram
                    if (listFunction>0 || dbAcc.RoleName=="sysadmin") //có tính năng đã phân quyền
                    {

                        //todo Kiểm tra quyền ở đây
                        return new OutEntity("ok", "");
                    }
                }


                return new OutEntity("", "Token không hợp lệ");
            }
            catch
            {
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng thử lại sau");
            }
        }
    }
}
