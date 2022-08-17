using KAS.API.MIDDEWARE.Entity;
using KAS.ECOS.API.Entity;
using Microsoft.AspNetCore.Mvc;

namespace KAS.ECOS.API.Controllers
{

    // [Route("api/KAS/ECOS")]
    public partial class EcosController : Controller
    {


        [HttpPost("Customer/Save")]
        public async Task<OutEntity> Customer_Save(InEntity ie)
        {
            try
            {
                var input = ie.getData<CustomerDTO>();

                using (var dbContextTransaction = w.Database.BeginTransaction())
                {

                    var tmp = w.Customers.FirstOrDefault(ii => ii.Id==input.ID);
                    if (tmp==null)
                    {
                        var tmpParent = new KAS.Entity.DB.ECOS.Customer()
                        {
                            Address = input.Address,
                            HandPhone = input.HandPhone,
                            Description = input.Description,
                            Email = input.Email,
                            Name = input.Name,
                            ParentId = input.ParentID,
                            Id = input.ID,
                            CustomerCodeSmac = input.SMAC_CUSTOMERCODE

                        };

                        foreach (var item in input.profiles)
                        {
                            tmpParent.CustomersProfiles.Add(new KAS.Entity.DB.ECOS.CustomersProfile()
                            {
                                CustomerId = input.ID,
                                ProductId = item.KasProductID,
                                ProfileApi =Code.Core.DataHandle.Json.Json_SerializeObject(item.App),
                                ProfileDb = Code.Core.DataHandle.Json.Json_SerializeObject(item.DB),

                            });
                        }
                        w.Customers.Add(tmpParent);
                    }
                    else
                    {
                        tmp.Address = input.Address;
                        tmp.HandPhone = input.HandPhone;
                        tmp.Description = input.Description;
                        tmp.Email = input.Email;
                        tmp.Name = input.Name;
                        tmp.ParentId = input.ParentID;
                        tmp.Id = input.ID;
                        tmp.CustomerCodeSmac = input.SMAC_CUSTOMERCODE;
                        tmp.CustomersProfiles.Clear();//Xóa hết profiles cũ

                        foreach (var item in input.profiles) //Add lại profiles
                        {
                            tmp.CustomersProfiles.Add(new KAS.Entity.DB.ECOS.CustomersProfile()
                            {
                                CustomerId = input.ID,
                                ProductId = item.KasProductID,
                                ProfileApi =Code.Core.DataHandle.Json.Json_SerializeObject(item.App),
                                ProfileDb = Code.Core.DataHandle.Json.Json_SerializeObject(item.DB),

                            });
                        }
                    }



                    await w.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                    return new OutEntity("ok", "");
                }

            }
            catch (Exception ee)
            {
                return new OutEntity("", ee.Message);
            }
        }
    }
}
