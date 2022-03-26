using System;
using System.Collections.Generic;
using Geolocation.Domain.Declare;
using Geolocation.Domain.Dto;
using Geolocation.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Geolocation.App.Example
{
    internal class ExampleSchemaFilter : ISchemaFilter
    {
        private readonly JsonSerializerSettings _settings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() }
        };

        private static readonly AddressDto AddressDtoObject = new()
        {
            CorrelationId = Guid.NewGuid(),
            Value = "Белгородская обл, г Старый Оскол, мкр Дубрава квартал 3",
            UnrestrictedValue = "309502, Белгородская обл, г Старый Оскол, мкр Дубрава квартал 3",
            Country = "Россия",
            KladrId = "3100000200004070007",
            PostalCode = "309502",
            Type = AddressType.Work
        };

        private static readonly ProblemDetails ProblemDetailsObject = new()
        {
            Type = "Microsoft.AspNetCore.Http.BadHttpRequestException",
            Title = "One or more validation errors occurred",
            Status = 400
        };

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            object example = context.Type.Name switch
            {
                nameof(ProblemDetails) => ProblemDetailsObject,
                nameof(AddressDto) => AddressDtoObject,
                nameof(IAddress) => AddressDtoObject,
                _ => null
            };

            if (example is not null)
                schema.Example = new OpenApiString(JsonConvert.SerializeObject(example, _settings));
        }
    }
}