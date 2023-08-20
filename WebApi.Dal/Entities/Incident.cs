using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Dal.Entities
{
    public class Incident
    {
        [Key]
        public string IncidentName { get; set; }
        public string Description { get; set; }
        public int AccountID { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
