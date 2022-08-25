using KAS.API.MIDDEWARE.Entity;
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
                var acc = ie.getData<AccountDTO>();

                if (ie.KASProductName=="ECOS")
                {
                    var dbAccEcos = w.EcosUsers.FirstOrDefault(i => i.PhoneNumber == acc.us);
                    if (dbAccEcos!=null)
                    {
                        OutEntity data = await Core.SMS.Firebase.Send(acc.us, acc.recapcharToke);
                        return data;
                    }
                    else
                    {
                        return new OutEntity("", "Tài khoản không hợp lệ");
                    }
                }
                else if (ie.KASProductName=="HOS")
                {
                    //todo trả về danh sách quyền của user
                }
                else
                {
                    var dbAcc = w.RolesUsers.FirstOrDefault(ii => ii.User == acc.us && !ii.IsDeleted && ii.Password == acc.pw);

                    if (dbAcc!=null)  //Có tài khoản
                    {
                        //if (!string.IsNullOrEmpty(ie.Token) && (dbAcc.User != acc.us))
                        //{

                        //    return new OutEntity("", "Token không hợp lệ");
                        //}

                        var listFunction = w.ProductsFunctionsPermissions.Count(ii => ii.CustomerId == dbAcc.CustomerId
                        && ii.ProductId == ie.KASProductName
                        && ii.RoleName == dbAcc.RoleName && !ii.IsDeleted
                        && ii.Expired.HasValue ? DateTime.UtcNow < ii.Expired.Value : true
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
                                    if (ie.KASProductName=="ECOS")
                                    {
                                        dbAcc.TokenExpired = DateTime.UtcNow.AddDays(1);
                                    }
                                    else
                                    {
                                        dbAcc.TokenExpired = DateTime.UtcNow.AddDays(10);

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

                }
                return new OutEntity("", "Token không hợp lệ");
            }
            catch
            {
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng đăng nhập lại sau");
            }
        }
        /// <summary>
        /// Kiểm tra OTP firebase
        /// </summary>
        /// <param name="ie"></param>
        /// <returns></returns>
        [HttpPost("ChekOTPFirebase")]
        public async Task<OutEntity> ChekOTPFirebase(InEntity ie)
        {
            try
            {
                var acc = ie.getData<OTPDTO>();

                if (ie.KASProductName == "ECOS")
                {
                    OutEntity data = await Core.SMS.Firebase.Confirm(acc.otp, acc.sessionInfo);
                    if (string.IsNullOrEmpty(data.error))
                    {
                        var dbAccEcos = w.EcosUsers.FirstOrDefault(i => i.PhoneNumber == acc.phone);
                        var res = new
                        {
                            data = dbAccEcos,
                            token = ""
                        };
                        data = new OutEntity(Core.DataHandle.Json.Json_SerializeObject(res), "");
                    }
                    return data;
                }
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng đăng nhập lại sau");
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
        [HttpPost("Token/Check")]
        public OutEntity Token_Check(InEntity ie)
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


                    var listFunction = w.ProductsFunctionsPermissions.Count(ii => ii.CustomerId==dbAcc.CustomerId
                   && ii.ProductId == ie.KASProductName
                   && ii.RoleName==dbAcc.RoleName && !ii.IsDeleted
                   && ii.Expired.HasValue ? DateTime.UtcNow<ii.Expired.Value : true
                   );
                    //token nếu là ecos thì xác thực qua telegram
                    if (listFunction>0 || dbAcc.RoleName=="sysadmin") //có tính năng đã phân quyền
                    {

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ie"></param>
        /// <returns></returns>
        [HttpPost("Token/Reset")]
        public async Task<OutEntity> Token_Reset(InEntity ie)
        {
            try
            {
                // var acc = ie.getData<AccountEntity>();


                var dbAcc = w.RolesUsers.FirstOrDefault(ii => ii.Token==ie.Token && !ii.IsDeleted);

                if (dbAcc!=null)  //Có token
                {

                    var tmpDate = w.ProductsFunctionsPermissions.Where(ii => ii.CustomerId==dbAcc.CustomerId
                   && ii.ProductId == ie.KASProductName
                   && ii.RoleName==dbAcc.RoleName && !ii.IsDeleted
                   && ii.Expired.HasValue ? DateTime.UtcNow<ii.Expired.Value : true
                   ).Min(ii => ii.Expired);

                    using (var dbContextTransaction = w.Database.BeginTransaction())
                    {
                        if (ie.KASProductName=="ECOS")
                        {
                            dbAcc.TokenExpired = DateTime.UtcNow.AddDays(1);
                        }
                        else
                        {

                            if (tmpDate<DateTime.UtcNow)
                            {
                                return new OutEntity("", "Token không thể gia hạn");
                            }

                            dbAcc.TokenExpired = DateTime.UtcNow.AddDays(10);
                            if (tmpDate<dbAcc.TokenExpired)
                            {
                                dbAcc.TokenExpired = tmpDate;
                            }

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

                        return new OutEntity(new
                        {
                            token = dbAcc.Token,
                            Expired = dbAcc.TokenExpired
                        }, "");
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
