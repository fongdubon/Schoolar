namespace Schoolar.Common.Models
{
    using System;

    public class Teacher
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Enrollment { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime HireDate { get; set; }

        public string ImageUrl { get; set; }
    }
}
