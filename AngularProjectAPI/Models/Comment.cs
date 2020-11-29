using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string Message { get; set; }

        //Relations
        public Nullable<int> UserID { get; set; }
        public User User { get; set; } //Creator
        public Nullable<int> ArticleID { get; set; }
        public Article Article { get; set; }
    }
}
