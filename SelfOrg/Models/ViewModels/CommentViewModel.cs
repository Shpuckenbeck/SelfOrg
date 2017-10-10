using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfOrg.Models
{
    public class CommentViewModel
    {
        public Post post { get; set; }
        public IQueryable<Comment> comments { get; set; }
        public IQueryable<Criterion> crits { get; set; }
    }
}
