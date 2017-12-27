using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SelfOrg.Models
{
    public class RatingViewModel //модель для парсинга оценок. категория+значение. если рабоатет - пост не передаётся
    {
        public string criterion { get; set; }     //id критерия   
        public string rating { get; set; } //id рейтинга
        public string weight { get; set; }//важность критерия
        public string post { get; set; }//id оцениваемого поста

    }
    public class RecalcModel //мм, ещё костыль
    {
        public string Rating { get; set; }
    }

}