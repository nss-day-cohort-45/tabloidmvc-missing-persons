using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();
        Comment GetCommentById(int id);
        List<Comment> GetCommentsByPostId(int PostId);
        void Add(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(int id);
    }
}
