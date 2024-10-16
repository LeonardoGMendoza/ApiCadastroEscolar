using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApplication1.Model.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(b => b.DateOfBirth);
            RuleFor(b => b.Height).NotNull().NotEmpty();
            RuleFor(b => b.Weight).NotNull().NotEmpty();
            RuleFor(b => b.StudentName).NotNull().NotEmpty();
            RuleFor(b => b.Id).NotNull().NotEmpty();
        }
    }
}
