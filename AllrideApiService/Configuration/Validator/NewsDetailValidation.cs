using AllrideApiCore.Dtos.Select;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Configuration.Validator
{
    public class NewsDetailValidation:AbstractValidator<NewsDetailResponseDto>
    {
        public NewsDetailValidation()
        {

        }
    }
}
