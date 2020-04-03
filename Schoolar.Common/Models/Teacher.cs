namespace Schoolar.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class Teacher
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("enrollment")]
        public string Enrollment { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("hireDate")]
        public DateTime HireDate { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("fullImageUrl")]
        public Uri FullImageUrl { get; set; }
    }
}
