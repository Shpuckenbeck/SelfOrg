using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SelfOrg.Models
{
    public class RatingViewModel //модель для парсинга оценок. категория+значение. если рабоатет - пост не передаётся
    {
        public string id { get; set; }        
        public string value { get; set; } 
        public string weight { get; set; }
        public string post { get; set; }

    }
}