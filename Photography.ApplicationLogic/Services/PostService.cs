using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Photography.ApplicationLogic.Services
{
    public class PostService
    {
        private readonly IPostRepository postRepository;
        private readonly UserManager<Account> _userManager;

        public PostService(IPostRepository postRepository, UserManager<Account> userManager)
        {
            this.postRepository = postRepository;
            this._userManager = userManager;
        }

        public IEnumerable<Post> GetPosts()
        {
            return postRepository.GetAll();
        }
        public Post GetPostById(int id)
        {
            return postRepository.GetById(id);
        }
        public Post AddPost(Post postToAdd)
        {
            return postRepository.Add(postToAdd);
        }
        public Post UpdatePost(Post postToUpdate)
        {
            return postRepository.Update(postToUpdate);
        }
        public bool RemovePost(int id)
        {
            return postRepository.Remove(id);
        }
        public bool CheckPost(int id)
        {
            return postRepository.Exists(id);
        }

        public IEnumerable<int> GetPostIdsByString(string searchString)
        {
            return postRepository.GetIdsByString(searchString);
        }

        public IEnumerable<Post> GetPostByUserId(string id)
        {
            return postRepository.GetByUser(id);
        }
        public Post CreatePostWithDesc(ClaimsPrincipal user, string desc)
        {
            var post = new Post
            {
                Description = desc,
                Likes = 0,
                AccountId = _userManager.GetUserId(user),
            };
            postRepository.Add(post);
            return post;
        }

        public IEnumerable<int> GetPostIdByUserId(string id)
        {
            return postRepository.GetIdsByUserId(id);
        }
        public IEnumerable<int> GetPostIds()
        {
            return postRepository.GetIds();
        }
    }
}
