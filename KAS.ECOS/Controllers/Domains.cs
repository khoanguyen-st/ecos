using Amazon;
using Amazon.Route53;
using Amazon.Route53.Model;
using KAS.API.MIDDEWARE.Entity;
using KAS.ECOS.API.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
namespace KAS.ECOS.API.Controllers
{

    // [Route("api/KAS/ECOS")]
    public partial class EcosController : Controller
    {
        [HttpPost("/api/KAS/ECOS/Domains/CreateSubDomain")]
        public async Task<OutEntity> Domains_CreateSubDomain(InEntity ie)
        {
            try
            {
                var input = ie.getData<DomainInfoEntity>();
                var client = new AmazonRoute53Client("AKIA5W6NDETGLCGOSEDV", "ipRTjoaPc9cuJyLED6cpe24KDmCVh9kTqC+iM2Xv", Amazon.RegionEndpoint.APSoutheast1);
                var hostedzones = await client.ListHostedZonesAsync();
                foreach (var item in hostedzones.HostedZones)
                {
                    if (item.Name.TrimEnd('.')==input.domain)
                    {
                        var response = await client.ChangeResourceRecordSetsAsync(new ChangeResourceRecordSetsRequest
                        {
                            ChangeBatch = new ChangeBatch
                            {
                                Changes = new List<Change> {
                                new Change {
                                    Action = "CREATE",
                                    ResourceRecordSet = new ResourceRecordSet {
                                        Name = input.subDomain,
                                        ResourceRecords = new List<ResourceRecord> {
                                            new ResourceRecord { Value = input.ip }
                                        },
                                        TTL = 86400,
                                        Type = RRType.A
                                    }
                                }
                            },
                                Comment = "ECOS create domain automatic"
                            },
                            HostedZoneId = item.Id //"Z07291391KS2LNGCKFEB2"
                        });
                        ChangeInfo changeInfo = response.ChangeInfo;
                        break;
                    }
                }
                return new OutEntity("ok", "");
            }
            catch (Exception ee)
            {
                return new OutEntity("", ee.InnerException?.Message ?? ee.Message);
            }
        }


#if DEBUG
        [HttpPost("/api/Debug/TestJoin")]
        public async Task<OutEntity> Debug_TestJoin(InEntity ie)
        {
            try
            {

                var list = (from info in w.Dpconfigs
                            join item in w.DpconfigCustomers on info.Id equals item.ClientId
                            select new {info,item} into a
                            group a by a.info.Id into g
                            select new
                            {
                                Id = g.FirstOrDefault().info.Id,
                                Customer = g.Select(_=>_.item.CustomerId).ToList()
                            }
                            ).ToList();
                    return new OutEntity("ok", "");
            }
            catch (Exception ee)
            {
                return new OutEntity("", ee.InnerException?.Message ?? ee.Message);
            }
        }
#endif
    }


}
