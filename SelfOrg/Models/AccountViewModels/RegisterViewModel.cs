using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SelfOrg.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Задайте логин")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Display(Name = "Отображаемое имя")]
        public string displayedname { get; set; }

        [Required(ErrorMessage = "Задайте имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Задайте фамилию")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Задайте номер телефона")]
        //[Display(Name = "Телефон")]
        //public override string PhoneNumber { get; set; }



        [Display(Name = "Аватар")]
        public IFormFile Avatar { get; set; }



        [Required(ErrorMessage = "Задайте пароль")]
        [StringLength(100, ErrorMessage = "{0} должен иметь длину от {2} до {1} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
