using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SelfOrg.Models
{
    public enum Priority //важность критериев в оценке
    {   [Display(Name ="Низкая")]
        Low,
        [Display(Name = "Средняя")]
        Medium,
        [Display(Name = "Высокая")]
        High
    }
    public enum UserLevel //уровень пользователя
    {
        [Display(Name = "Гость")]
        guest,
        [Display(Name = "Пользователь")]
        regular,
        [Display(Name = "Подтверждённый пользователь")]
        confirmed,
        [Display(Name = "Админ")]
        admin
    }
    /// <summary>
    /// Пользователь системы. Наследовано от IdentityUser с переопределением
    /// поля UserName для изменения отображения.
    /// </summary>
    public class User : IdentityUser
    {
        //[Required(ErrorMessage = "Задайте имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Задайте фамилию")]
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
        public DateTime RegDate { get; set; } //дата регистрации
        [Display(Name="Вес")]
        public float Weight { get; set; } //вес голоса пользователя
        [Display(Name ="Уровень")]
        public UserLevel level { get; set; } //уровень - гость, базовый, подтверждённый, администратор
        [Display(Name = "Отчество")]
        public string Middlename { get; set; } //отчество
    }

    /// <summary>
    /// Класс поста системы
    /// </summary>
    public class Post
    {
        public int PostID { get; set; }
        [Required(ErrorMessage = "Введите название")]
        [Display(Name = "Название")]
        public string PostName { get; set; } //название поста
        [Required(ErrorMessage = "Введите текст")]
        [Display(Name = "Текст")] 
        public string content { get; set; } //текст поста
        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "Описание")]
        public string PostDescr { get; set; } //краткое содержание поста
        [Required(ErrorMessage = "Выберите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Опубликовано")]
        public DateTime PostDate { get; set; } //дата публикации
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
        public User User { get; set; } //автор поста
        [Display(Name = "Рейтинг")]
        public float rating { get; set; } 

        public virtual ICollection<PostTag> PostTags { get; set; }
    }

    /// <summary>
    /// Класс комментария к посту
    /// </summary>
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
        public int rating { get; set; } //не дублирует commrates; она нужна только для отслеживания
        [Required(ErrorMessage = "Укажите пост")]
        [Display(Name = "Пост")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public DateTime? LastModified { get; set; } //дата изменения
        public int? ReplyTo { get; set; } //для ответов на комментарии; указывает на id комментария, на который отвечают
    }

    /// <summary>
    /// Категория поста
    /// </summary>
    public class Category
    {

        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Введите название категории")]
        [Display(Name = "Название")]
        public string CatName { get; set; }
        [Required(ErrorMessage = "Введите описание категории")]
        [Display(Name = "Описание")]
        public string CatDescr { get; set; }
        public int postcount { get; set; } //количество постов в категории
    }

    /// <summary>
    /// Изображение
    /// </summary>
    public class Pic
    {
        public int PicId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }

    /// <summary>
    /// Тег
    /// </summary>
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagSlug { get; set; }
       
    }

    /// <summary>
    /// Тег поста. Указывает ан связь между Tag и Post
    /// </summary>
    public class PostTag
    {
        public int PostTagId { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
    /// <summary>
    /// Критерий оценки
    /// </summary>
    public class Criterion
    {
        public int CriterionId { get; set; }
        [Required(ErrorMessage = "Задайте название")]
        [Display(Name = "Критерий")]
        public string name { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание")]
        public string description { get; set; }
        //[Display(Name = "Важность")]
        //public Priority prio { get; set; }
    }

    /// <summary>
    /// Оценка пользователем поста по критерию
    /// </summary>
    public class Rating 
    {
        public int RatingId { get; set; }
        public int CriterionId { get; set; }
        public Criterion Criterion { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public float rating { get; set; }
    }

    /// <summary>
    /// Множитель голоса
    /// </summary>
    public class Multiplier //вес голоса по рейтингу
    {
        public int MultiplierId { get; set; }
        public int Lower { get; set; }
        public int Higher { get; set; }
        public float Mul { get; set; }
    }

    /// <summary>
    /// Вес критерия при оценке в конкретной категории. Указывает
    /// на связь между Category и Criterion
    /// </summary>
    public class CatCrit //критерии категорий
    {
        public int CatCritId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CriterionId { get; set; }
        public Criterion Criterion { get; set; }
        public Priority prio { get; set; }
    }

    /// <summary>
    /// Оценка комментария пользователем
    /// </summary>
    public class CommRate //рейтинг комментария
    {
        public int CommRateId { get; set; }
        public int value { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
