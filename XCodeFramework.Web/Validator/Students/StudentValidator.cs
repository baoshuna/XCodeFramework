using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCodeFramework.Web.Models.Student;

namespace XCodeFramework.Web.Validator.Students
{
    public class StudentValidator : AbstractValidator<StudentModel>
    {
        public StudentValidator()
        {
            RuleFor(it => it.Name).NotNull().Length(1, 8);
            RuleFor(it => it.Age).NotNull();
            RuleFor(it => it.Sex).NotNull();
        }
    }
}