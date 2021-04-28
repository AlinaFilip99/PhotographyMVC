using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;

namespace Photography.ApplicationLogic.Services
{
    public class CommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
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
    }
}
