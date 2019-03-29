using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Domain.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedTime = DateTime.Now;
        }

        //[Editable(false)]
        public DateTime? CreatedTime { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime? ModifyTime { get; set; }
        
        public string ModifyBy { get; set; }
    }
}
