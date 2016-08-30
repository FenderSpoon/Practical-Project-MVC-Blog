using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Blog.Models
{
    public class BigModel
    {

        public IEnumerable<MVC_Blog.Models.Post> PostModel { get; set; }
        public IEnumerable<MVC_Blog.Models.Comment> CommentModel { get; set; }
    }
}
