using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, IUserProfileRepository userProfileRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _userProfileRepository = userProfileRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
        }

        public IActionResult MyPosts(int userProfileId)
        {
            int userId = GetCurrentUserProfileId();
            var posts = _postRepository.GetPostsByUserProfileId(userId);
            return View(posts);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAllCategories();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAllCategories();
                return View(vm);
            }
        }


        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            int userId = GetCurrentUserProfileId();
            int userTypeId = _userProfileRepository.GetById(userId).UserTypeId;
            Post userPost = _postRepository.GetUserPostById(id, userId);
            Post anyPost = _postRepository.GetAnyPostById(id);

            if (userTypeId == 1)
            {
              return View(anyPost);
            }
            else if (userPost != null)
            {
              return View(userPost);
            }
            else
            {
              return RedirectToAction("Index");
            }
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            int userId = GetCurrentUserProfileId();
            int userTypeId = _userProfileRepository.GetById(userId).UserTypeId; 
            Post userPost = _postRepository.GetUserPostById(id, userId);

            if(userTypeId == 1)
            {
                try
                {
                    _postRepository.DeletePost(id);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
            else if(userPost != null)
            {
                try
                {
                    _postRepository.DeletePost(id);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(post);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
