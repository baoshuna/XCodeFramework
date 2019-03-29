using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Domain.BlogUsers
{
    [Table("BlogUsers")]
    public class BlogUser
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
