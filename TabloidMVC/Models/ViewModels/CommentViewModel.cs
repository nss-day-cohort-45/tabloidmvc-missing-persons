using System;
using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class CommentViewModel
    {
        public Post Post { get; set; }
        public Comment Comment { get; set; }
        public List<Comment> PostComments { get; set; }
    }
}
