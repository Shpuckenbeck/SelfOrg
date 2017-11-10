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
        public CommentsModel commmodel { get; set; }
        public IQueryable<CatCrit> crits { get; set; }
    }

    public class CommentsModel
    {
        public IQueryable<Comment> comments { get; set; }
    }
}
