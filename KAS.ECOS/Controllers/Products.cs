using KAS.API.MIDDEWARE.Entity;
using Microsoft.AspNetCore.Mvc;


namespace KAS.ECOS.API.Controllers
{
    //  [Route("api/KAS/ECOS")]
    public partial class EcosController : Controller
    {

        [HttpPost("Products/Get")]
        public OutEntity Products_Get(InEntity ie)
        {
            try
            {
                return new OutEntity(w.KasProducts.Select(ii => new { ii.Id, ii.Description }).ToList(), "");
            }
            catch
            {
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng thử lại sau");
            }
        }

        /// <summary>
        /// Sản phẩm của KAS chỉ có thể thêm, không thể xóa, vì là table cha của tất cả các table
        /// </summary>
        /// <param name="ie"></param>
        /// <returns></returns>
        [HttpPost("Products/Save")]
        public async Task<OutEntity> Products_Save(InEntity ie)
        {
            try
            {
                var tmpList = ie.getData<List<KAS.Entity.DB.ECOS.KasProduct>>();
                bool isSave = false;
                using (var dbContextTransaction = w.Database.BeginTransaction())
                {
                    foreach (var item in tmpList)
                    {
                        var dbItem = w.KasProducts.FirstOrDefault(ii => ii.Id == item.Id);

                        if (dbItem == null)
                        {
                            isSave=true;
                            w.KasProducts.Add(new KAS.Entity.DB.ECOS.KasProduct()
                            {
                                Id = item.Id.ToUpper(),
                                Description = item.Description
                            });
                        }
                        else
                        {
                            dbItem.Description = item.Description;
                        }
                    }
                    if (isSave)
                    {
                        await w.SaveChangesAsync();
                        await dbContextTransaction.CommitAsync();
                    }

                }
                return new OutEntity("ok", "");
            }
            catch
            {
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng thử lại sau");
            }
        }


        [HttpPost("Products/Function/Get")]
        public OutEntity Products_Function_Get(InEntity ie)
        {
            try
            {
                return new OutEntity(w.KasProductsFunctions.Where(ii => ii.KasProductId==ie.data && !ii.IsDeleted).Select(ii => new
                {
                    ii.KasProductId,
                    ii.FunctionName,
                    ii.FunctionParent,
                    ii.FunctionPath,
                    ii.Description,
                    ii.Level
                }).ToList(), "");
            }
            catch
            {
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng thử lại sau");
            }
        }
        [HttpPost("Products/Function/Save")]
        public async Task<OutEntity> Products_Function_Add(InEntity ie)
        {
            try
            {
                var item = ie.getData<KAS.Entity.DB.ECOS.KasProductsFunction>();
                using (var dbContextTransaction = w.Database.BeginTransaction())
                {

                    var dbItem = w.KasProductsFunctions.FirstOrDefault(ii => ii.FunctionName == item.FunctionName && ii.KasProductId == ie.KASProductName);

                    if (dbItem == null)
                    {

                        w.KasProductsFunctions.Add(new KAS.Entity.DB.ECOS.KasProductsFunction()
                        {
                            KasProductId = ie.KASProductName,
                            FunctionName = item.FunctionName,
                            Description=item.Description,
                            FunctionParent=item.FunctionParent,
                            FunctionPath=item.FunctionPath,
                            Level=item.Level,
                        });
                    }
                    else
                    {
                        dbItem.FunctionName = item.FunctionName;
                        dbItem.Description=item.Description;
                        dbItem.FunctionParent=item.FunctionParent;
                        dbItem.FunctionPath=item.FunctionPath;
                        dbItem.Level=item.Level;
                    }
                    await w.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                    return new OutEntity("ok", "");

                }

            }
            catch
            {
                return new OutEntity("", "Hiện tại hệ thống đang bận, vui lòng thử lại sau");
            }
        }
    }
}
