using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfOrg.Models
{
    public class CommentRateModel
    {
        public string commentid { get; set; } //id комментария
        public string action { get; set; } //тип оценки
    }
}
