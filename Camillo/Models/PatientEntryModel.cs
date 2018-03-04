using Camillo.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Camillo.Models
{
    public class IsoDateConverter : IsoDateTimeConverter
    {
        public IsoDateConverter()
        {
            this.DateTimeFormat = "yyyy/MM/dd";
        }
    }

    public class PatientEntryModel
    {

        public int AccountId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MedicareNumber { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PatientStatus Status { get; set; }

        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime? BirthDate { get; set; }

        public string PhotoUrl { get; set; }

        public int? Age
        {
            get
            {
                if (!BirthDate.HasValue) return null;
                var reference = DateTime.Now.Date;
                int age = reference.Year - BirthDate.Value.Year;
                if (reference < BirthDate.Value.AddYears(age)) age--;
                return age;
            }
        }

        public DateTime RegisterDate { get; set; }
        public string Email { get; internal set; }
        public string Username { get; internal set; }
        public string Password { get; internal set; }
    }
}