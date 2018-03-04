using Camillo.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Camillo.Models
{
    public class PatientSummary
    {
        public int AccountId { get; set; }
        public DateTime RegisterDate {get;set;}
        public string FirstName {get;set;}
        public string LastName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PatientStatus Status { get; set; }
        public string PhotoUrl { get; set; }
    }
}