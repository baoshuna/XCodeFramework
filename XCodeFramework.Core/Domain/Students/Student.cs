using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Domain.Common
{
    [Table("Students")] // 默认使用类名，加特性覆盖类名
    public class Student : BaseEntity
    {
        [Key]
        [Required]
        public string Id { get; set; }

        // [Column("NickName")] 这里相应的，Query里面的sql语句需要给列指定别名:Select NickName as Name
        public string Name { get; set; }

        public int Age { get; set; }

        public int Sex { get; set; }
    }
}
