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
        [HttpPost("Products/Set")]
        public async Task<OutEntity> Products_Set(InEntity ie)
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
                                Id = item.Id,
                                Description = item.Description
                            });
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
    }
}
