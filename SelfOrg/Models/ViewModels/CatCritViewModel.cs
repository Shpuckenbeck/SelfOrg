using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SelfOrg.Models
{
    public class CatCritViewModel
    {
        public Category category { get; set; }
        public int catid { get; set; }
        [Display(Name="Критерий")]
        public IQueryable<Criterion> crits { get; set; }
        //public List<CatCrit> catcrits { get; set; }
        public int[] critid { get; set; }
        public Priority[] prio { get; set; }
    }

    public class CritPostModel
    {
        public string category { get; set; }
        public string criteria { get; set; }
        public string weight { get; set; }
    }
}
