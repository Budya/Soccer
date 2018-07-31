using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerId.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string PasswordConfirm { get; set; }

        /// /////////////////////////
        /// [Required]
        [Display(Name = "Выбор команды")]
        IEnumerable<SoccerId.Entities.Team> Teams { get; set; }
        public string TeamName { get; set; }
        /// ///                  
    }
    public class SelectTeam
    {
        [Required]
        [Display(Name = "Выбор команды")]
        public string TeamName { get; set; }
    }


    //public class RegisterModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    public string Email {
    //        get
    //        {
    //            return "email@mail.com";
    //        }
    //        set { } }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get { return "P@sword2"; } set { } }

    //    [Required]
    //    [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
    //    [DataType(DataType.Password)]
    //    public string PasswordConfirm { get { return "P@sword2"; } set { } }
    //}

    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class CreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class DeleteModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class EditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}