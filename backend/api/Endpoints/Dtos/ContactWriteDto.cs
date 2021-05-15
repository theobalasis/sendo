using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Endpoints.Dtos
{
    public class ContactWriteDto
    {
        public string MailAddress { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}