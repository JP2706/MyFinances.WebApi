﻿using MyFinances.Core.Dtos;
using MyFinances.WebApi.Models.Domains;
using System.Collections.Generic;
using System.Linq;

namespace MyFinances.WebApi.Models.Converters
{
    public static class OperationConverter
    {
        public static OperationDto ToDto(this Operation model)
        {
            return new OperationDto
            {
                CategoryId = model.CategoryId,
                Date = model.Date,
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Value = model.Value,
            };
        }

        public static IEnumerable<OperationDto> ToDtos(this  IEnumerable<Operation> model) 
        {
            if(model == null) 
                return Enumerable.Empty<OperationDto>();

            return model.Select(x => x.ToDto());
        }

        public static Operation ToDao(this OperationDto model)
        {
            return new Operation
            {
                CategoryId = model.CategoryId,
                Date = model.Date,
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Value = model.Value,
            };
        }
    }
}
