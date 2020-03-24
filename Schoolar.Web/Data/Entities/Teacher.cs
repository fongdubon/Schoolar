namespace Schoolar.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Teacher : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(50, ErrorMessage = "{0} the field must have a maximum of {1} characters")]
        [Display(Name = "Hire date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }

        public User User { get; set; }
    }
}

