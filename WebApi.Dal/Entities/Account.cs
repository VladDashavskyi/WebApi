using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WebApi.Dal.Entities
{
    public  class Account
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }
        public string Name { get; set; }
        public int? ContactID { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual Incident Incident { get; set; }
    }
}
