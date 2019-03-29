using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Domain.Users
{
    [Table("Users")]
    public class User
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [MaxLength(50)]
        //[NotMapped]
        public String Name { get; set; }
    }
}
