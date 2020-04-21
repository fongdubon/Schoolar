﻿namespace Schoolar.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Teacher : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }
        //cambio
        public string FullImageUrl => string.IsNullOrEmpty(ImageUrl) ? null : $"https://schoolarumad.azurewebsites.net{ImageUrl.Substring(1)}";

        public User User { get; set; }
    }
}

