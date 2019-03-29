using FluentValidation.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XCodeFramework.Web.Validator.Students;

namespace XCodeFramework.Web.Models.Student
{
    [Validator(typeof(StudentValidator))]
    [DisplayName("学生")]
    public class StudentModel
    {
        [DisplayName("编号")]
        public string Id { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("年龄")]
        public int Age { get; set; }

        [DisplayName("性别")]
        public int Sex { get; set; }
    }
}