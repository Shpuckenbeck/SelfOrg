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
        public bool rateable { get; set; } //может ли пользователь оценить пост (если автор/уже голосовал - нет)
        public float userrating { get; set; } //оценка, данная пользователем посту - временное решение?
        public bool editable { get; set; } //определяет возможность редактирования поста
        public bool islogged { get; set; } //залогинен ли
    }

    public class CommentsModel
    {
        public IQueryable<Comment> comments { get; set; }
        public int[] commrates { get; set; } //оценки комментариев ДАННЫМ пользователем
    }
}
