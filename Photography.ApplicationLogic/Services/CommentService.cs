using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Services
{
    public class CommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<Account> userManager;

        public CommentService(ICommentRepository commentRepository, UserManager<Account> userManager)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
        }

        public IEnumerable<Comment> GetComments()
        {
            return commentRepository.GetAll();
        }
        public Comment GetCommentById(int id)
        {
            return commentRepository.GetById(id);
        }
        public Comment AddComment(Comment commentToAdd)
        {
            return commentRepository.Add(commentToAdd);
        }
        public Comment UpdateComment(Comment commentToUpdate)
        {
            return commentRepository.Update(commentToUpdate);
        }
        public bool RemoveComment(int id)
        {
            return commentRepository.Remove(id);
        }
        public bool CheckComment(int id)
        {
            return commentRepository.Exists(id);
        }
        public IEnumerable<Comment> GetCommentByPostId(int id)
        {
            return commentRepository.GetByPost(id);
        }

        public async Task<Comment> CreateComment(ClaimsPrincipal user, string message, int id)
        {
            var account = await userManager.GetUserAsync(user);
            var comm = new Comment
            {
                CommMessage = message,
                UserName = account.UserName,
                PostId = id
            };
            return commentRepository.Add(comm);
        }
    }
}
