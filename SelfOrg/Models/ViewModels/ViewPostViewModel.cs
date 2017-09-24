using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SelfOrg.Models

{
    public class ViewPostViewModel
    {
        //public List<int> postid;
        //public List<string> postname;
        //public List<string> postcontent;
        //public List<string> postdescr;
        //public List<string> postdate;
        //public List<string> postcategory;
        //public List<string> username;
        //public List<string[]> tags;
        public List<Post> posts;
        public List<Category> categories;
        public List<User> users;
        public List<List<Tag>> tags;

    }
}
