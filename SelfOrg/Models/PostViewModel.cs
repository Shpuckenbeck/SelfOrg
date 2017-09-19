using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SelfOrg.Models
{
    public class PostViewModel
    {
        
            
            [Display(Name = "Название")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Введите текст")]
            [Display(Name = "Текст")]
            public string Text { get; set; }

            [Display(Name = "Категория")]
            public int Cat { get; set; }

        
    }
}
