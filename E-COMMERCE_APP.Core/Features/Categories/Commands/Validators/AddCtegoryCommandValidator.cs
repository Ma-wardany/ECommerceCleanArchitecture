﻿using E_COMMERCE_APP.Core.Features.Categories.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Categories.Commands.Validators
{
    public class AddCtegoryCommandValidator : AbstractValidator<AddCtegoryCommand>
    {
        public AddCtegoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("category name must not be empty!")
                .NotNull().WithMessage("category name is required!");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("category Description must not be empty!")
                .NotNull().WithMessage("category Description is required!");
        }
    }
}
