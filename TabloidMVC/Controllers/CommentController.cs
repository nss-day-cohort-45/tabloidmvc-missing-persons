using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Repositories;
using System.Collections.Generic;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Models;
using System;
using System.Security.Claims;
using Microsoft.VisualBasic;

namespace TabloidMVC.Controllers
{
    
    public class CommentController : Controller
    {

        private readonly ICommentRepository _commentRepo;
        private readonly IPostRepository _postRepository;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public CommentController(
            ICommentRepository commentRepository,
            IPostRepository postRepository)
        {
            _commentRepo = commentRepository;
            _postRepository = postRepository;
        }

        // GET: CommentController
        public ActionResult Index(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);

            if (post == null)
            {
                return NotFound();
            }

            List<Comment> comments = _commentRepo.GetCommentsByPostId(id);

            CommentViewModel vm = new CommentViewModel()
            {
                Post = post,
                PostComments = comments
            };

            return View(vm);
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            var vm = new CommentViewModel();
            return View(vm);
        }

        // POST: Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(CommentViewModel vm)
        {
            try
            {
                vm.Comment.CreationDate = DateAndTime.Now;
                
                vm.Comment.UserProfileId = GetCurrentUserProfileId();

                _commentRepo.Add(vm.Comment);

                return RedirectToAction("Index", new { id = vm.Comment.Id });
            }
            catch
            {
                
                return View(vm);
            }
        }



        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            Comment comment = _commentRepo.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                _commentRepo.EditComment(comment);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(comment);
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            Comment comment = _commentRepo.GetCommentById(id);

            return View(comment);
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
