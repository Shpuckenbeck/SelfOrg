using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SelfOrg.Models
{
    public enum Priority //важность критериев в оценке
    {
        Low,
        Medium,
        High
    }
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Задайте имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Задайте фамилию")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Задайте логин")]
        [Display(Name = "Логин")]
        public override string UserName { get; set; }
        //[Required(ErrorMessage = "Задайте номер телефона")]
        //[Display(Name = "Телефон")]
        //public override string PhoneNumber { get; set; }
        [Display(Name = "Отображаемое имя")]
        public string displayedname { get; set; }
        [Display(Name = "Рейтинг")]
        public float rating { get; set; }
        //[Required(ErrorMessage = "Выберите аватар")]
        [Display(Name = "Аватар")]
        public string Avatar { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegDate { get; set; }
    }

    public class Post
    {

        public int PostID { get; set; }
        [Required(ErrorMessage = "Введите название")]
        [Display(Name = "Название")]
        public string PostName { get; set; }
        [Required(ErrorMessage = "Введите текст")]
        [Display(Name = "Текст")]
        public string content { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "Описание")]
        public string PostDescr { get; set; }
        [Required(ErrorMessage = "Выберите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Опубликовано")]
        public DateTime PostDate { get; set; }
        public string PostSlug { get; set; } //url-представление
        public DateTime? LastModified { get; set; } //дата изменения
        [Required(ErrorMessage = "Выберите категорию")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        [Display(Name = "Категория")]
        public Category Category { get; set; } //ссылка на категорию
        [Display(Name = "Автор")]
        public string UserId { get; set; }
        [Display(Name = "Автор")]
        public User User { get; set; }
        [Display(Name = "Рейтинг")]
        public float rating { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Автор")]
        public User User { get; set; }
        [Required(ErrorMessage = "Введите текст")]
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Выберите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Опубликовано")]
        public DateTime CommentDate { get; set; }
        [Required(ErrorMessage = "Укажите пост")]
        [Display(Name = "Пост")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public DateTime? LastModified { get; set; } //дата изменения
        public int? ReplyTo { get; set; } //будет использоваться в дальнейшем для ответов на комментарии; указывает на id комментария, на который отвечают
    }

    public class Category
    {

        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Введите название категории")]
        [Display(Name = "Название")]
        public string CatName { get; set; }
        [Required(ErrorMessage = "Введите описание категории")]
        [Display(Name = "Описание")]
        public string CatDescr { get; set; }
    }

    public class Pic
    {
        public int PicId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagSlug { get; set; }
       
    }
    public class PostTag
    {
        public int PostTagId { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }

    public class Criterion
    {
        public int CriterionId { get; set; }
        [Required(ErrorMessage = "Задайте название")]
        [Display(Name = "Критерий")]
        public string name { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание")]
        public string description { get; set; }
        [Display(Name = "Важность")]
        public Priority prio { get; set; }
    }
}
