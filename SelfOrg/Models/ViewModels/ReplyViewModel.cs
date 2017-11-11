using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfOrg.Models
{
    public class ReplyViewModel //также используется для создания родительских комментариев, тогда CommentId хранит id поста, а не комментария
    {
       public string comment;
      public  string id;
     

    }

    public class ResultViewModel
    {
        public string CommentId;
    }
}
