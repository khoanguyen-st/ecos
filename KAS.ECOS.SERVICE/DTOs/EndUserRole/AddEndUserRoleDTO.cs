using AutoMapper;
using KAS.Entity.DB.ECOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAS.ECOS.SERVICE.DTOs.GrantUser
{
    public class AddEndUserRoleDTO
    {
        public Guid? UserDeviceId { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
