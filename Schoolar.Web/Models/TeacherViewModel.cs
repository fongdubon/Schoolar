namespace Schoolar.Web.Models
{
    using Web.Data.Entities;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class TeacherViewModel : Teacher
    {
        //[Required(ErrorMessage = "{0} es required")]
        //[Display(Name = "Email")]
        //[DataType(DataType.EmailAddress)]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "{0} es required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "thi field {0} must have between {2} and {1} characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} es required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "thi field {0} must have between {2} and {1} characters")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirma password")]
        public string PasswordConfirm { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}