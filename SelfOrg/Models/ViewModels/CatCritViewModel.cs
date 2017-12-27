using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SelfOrg.Models
{
    public class CatCritViewModel
    {
        public Category category { get; set; } //категория
        public int catid { get; set; } //отдельно - id категории
        [Display(Name="Критерий")]
        public IQueryable<Criterion> crits { get; set; } //доступные критерии
        //public List<CatCrit> catcrits { get; set; }
        public int[] critid { get; set; } //отдельно - массив id критериев
        public Priority[] prio { get; set; } //массив значений приоритета
    }

    //public class CritPostModel
    //{
    //    public string category { get; set; }
    //    public string criteria { get; set; }
    //    public string weight { get; set; }
    //}
}
