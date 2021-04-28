using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;

namespace Photography.ApplicationLogic.Services
{
    public class PostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
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
    }
}
