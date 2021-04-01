using System;
using System.ComponentModel;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [DisplayName("Comment By:")]
        public string AuthorName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
