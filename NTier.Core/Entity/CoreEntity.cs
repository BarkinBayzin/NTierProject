using NTier.Core.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Core.Entity
{
    public class CoreEntity : IEntity<Guid>
    {
        public CoreEntity()
        {
            this.Status = Status.Active;
            this.CreatedDate = DateTime.Now;
            this.CreatedAtUserName = WindowsIdentity.GetCurrent().Name;
            this.CreatedComputerName = Environment.MachineName;
            this.CreatedIp = "123";
            this.CreatedBy = 1;
        }
        public Guid Id { get; set; }
        public Guid? MasterId { get; set; }
        public Status Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIp { get; set; }
        public string CreatedAtUserName { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIp { get; set; }
        public string ModifiedAtUserName { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
