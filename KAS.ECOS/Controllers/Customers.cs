using Amazon;
using Amazon.Route53;
using Amazon.Route53.Model;
using KAS.ECOS.MIDDLEWARE.Entity;
using KAS.ECOS.API.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace KAS.ECOS.API.Controllers
{

    // [Route("api/KAS/ECOS")]
    public partial class EcosController : Controller
    {
        [HttpPost("/api/KAS/ECOS/Customer/Save")]
        public async Task<OutEntity> Customer_Save(InEntity ie)
        {
            try
            {
                var input = ie.getData<CustomerDTO>();

                using (var dbContextTransaction = w.Database.BeginTransaction())
                {

                    var tmp = w.Customers.FirstOrDefault(ii => ii.Id == input.ID);
                    if (tmp == null)
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
                                ProfileApi = Code.Core.DataHandle.Json.Json_SerializeObject(item.App),
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
                                ProfileApi = Code.Core.DataHandle.Json.Json_SerializeObject(item.App),
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
        [HttpPost("/api/KAS/ECOS/Customer/GetDPConfig")]
        public async Task<OutEntity> Customer_GetDPConfig(InEntity ie)
        {
            try
            {
                string input = ie.data;
                var tmp = w.Dpconfigs.Include(_=>_.DpconfigCustomers).Where(ii => ii.Id == input && !ii.IsDeleted)
                    .Select(_=> new SmacConfigEntity()
                    {
                        Id = _.Id,
                        AppRun = _.AppRun,
                        CreateSmacAppRun = _.CreateSmacAppRun,
                        CreateSmacServerCode = _.CreateSmacServerCode,
                        Customers = _.DpconfigCustomers != null ? _.DpconfigCustomers.Select(_ => new SmacConfigEntityItem()
                        {
                            CustomerID = _.CustomerId,
                            ClientID=_.ClientId,
                            ExcuteCostPriceDB = _.ExcuteCostPriceDb != null && _.ExcuteCostPriceDb.Length > 0 ? _.ExcuteCostPriceDb.ToList() : new List<short>(),
                            SAPMaiSonConfig = _.SapmaiSonConfig
                        }).ToList() : new List<SmacConfigEntityItem>(),
                        MaxSecondSendLog = _.MaxSecondSendLog,
                        MaxThread = _.MaxThread,
                        MaxThreadBill = _.MaxThreadBill,
                        MaxThreadImport = _.MaxThreadImport,
                        MongoHost = _.MongoHost,
                        MonitorHost = _.MonitorHost,
                        SyncApi = _.SyncApi,
                        PostgreHost=_.PostgreHost,
                        BackupConfig=_.BackupConfig,
                        
                    }).FirstOrDefault();
                if (tmp!=null)
                {
                    return new OutEntity(tmp, "");
                }
                //if (tmp != null)
                //{
                //    return new OutEntity(new SmacConfigEntity()
                //    {
                //        Id = tmp.Id,
                //        AppRun = tmp.AppRun,
                //        CreateSmacAppRun = tmp.CreateSmacAppRun,
                //        CreateSmacServerCode = tmp.CreateSmacServerCode,
                //        Customers = tmp.DpconfigCustomers != null ? tmp.DpconfigCustomers.Select(_ => new SmacConfigEntityItem()
                //        {
                //            CustomerID = _.CustomerId,
                //            ExcuteCostPriceDB = _.ExcuteCostPriceDb != null && _.ExcuteCostPriceDb.Length > 0 ? _.ExcuteCostPriceDb?.ToList() : new List<short>()
                //        }).ToList() : new List<SmacConfigEntityItem>(),
                //        MaxSecondSendLog = tmp.MaxSecondSendLog,
                //        MaxThread = tmp.MaxThread,
                //        MaxThreadBill = tmp.MaxThreadBill,
                //        MaxThreadImport = tmp.MaxThreadImport,
                //        MongoHost = tmp.MongoHost,
                //        MonitorHost = tmp.MonitorHost,
                //        SyncApi = tmp.SyncApi
                //    }, "");
                //}
                return new OutEntity("", "Không tìm thấy cấu hình");
            }
            catch (Exception ee)
            {
                return new OutEntity("", ee.Message);
            }
        }
        [HttpPost("/api/KAS/ECOS/Customer/SetDPConfig")]
        public async Task<OutEntity> Customer_SetDPConfig(InEntity ie)
        {
            try
            {
                var input = ie.getData<SmacConfigEntity>();
                var tmp = w.Dpconfigs.FirstOrDefault(ii => ii.Id == input.Id && !ii.IsDeleted);
                if (tmp == null)
                {
                    await w.Dpconfigs.AddAsync(new KAS.Entity.DB.ECOS.Dpconfig()
                    {
                        Id = input.Id,
                        AppRun=input.AppRun,
                        CreateSmacAppRun=input.CreateSmacAppRun,
                        CreateSmacServerCode=input.CreateSmacServerCode,
                        MaxSecondSendLog=input.MaxSecondSendLog,
                        MaxThread=input.MaxThread,
                        MongoHost = input.MongoHost,
                        MaxThreadBill=input.MaxThreadBill,
                        MaxThreadImport=input.MaxThreadImport,
                        MonitorHost=input.MonitorHost,
                        SyncApi=input.SyncApi,
                        PostgreHost=input.PostgreHost,
                        BackupConfig=input.BackupConfig,
                        IsDeleted = false
                    });
                }
                else
                {
                    tmp.AppRun = input.AppRun;
                    tmp.CreateSmacAppRun = input.CreateSmacAppRun;
                    tmp.CreateSmacServerCode = input.CreateSmacServerCode;
                    tmp.MaxSecondSendLog = input.MaxSecondSendLog;
                    tmp.MaxThread = input.MaxThread;
                    tmp.MongoHost = input.MongoHost;
                    tmp.MaxThreadBill = input.MaxThreadBill;
                    tmp.MaxThreadImport = input.MaxThreadImport;
                    tmp.MonitorHost = input.MonitorHost;
                    tmp.SyncApi = input.SyncApi;
                    tmp.PostgreHost = input.PostgreHost;
                    tmp.BackupConfig = input.BackupConfig;
                    tmp.IsDeleted = false;
                }
                await w.SaveChangesAsync();
                return new OutEntity("ok", "");
            }
            catch (Exception ee)
            {
                return new OutEntity("", ee.Message);
            }
        }
        [HttpPost("/api/KAS/ECOS/Customer/SetDPConfigCustomer")]
        public async Task<OutEntity> Customer_SetDPConfigCustomer(InEntity ie)
        {
            try
            {
                var input = ie.getData<SmacConfigEntityItem>();
                var tmp = w.DpconfigCustomers.FirstOrDefault(_ => _.CustomerId == input.CustomerID);
                if (tmp ==null)
                {
                    await w.DpconfigCustomers.AddAsync(new KAS.Entity.DB.ECOS.DpconfigCustomer()
                    {
                        ClientId = input.ClientID,
                        CustomerId = input.CustomerID,
                        ExcuteCostPriceDb = input.ExcuteCostPriceDB.ToArray(),
                        SapmaiSonConfig=input.SAPMaiSonConfig
                    });
                }
                else
                {
                    tmp.ClientId = input.ClientID;
                    tmp.SapmaiSonConfig = input.SAPMaiSonConfig;
                    tmp.ExcuteCostPriceDb = input.ExcuteCostPriceDB.ToArray();
                }
                await w.SaveChangesAsync();
                return new OutEntity("ok", "");
            }
            catch (Exception ee)
            {
                return new OutEntity("", ee.Message);
            }
        }
    }
}
