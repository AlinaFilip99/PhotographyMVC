using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Services
{
    public class PhotoService
    {
        private readonly IPhotoRepository photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return photoRepository.GetAll();
        }
        public Photo GetPhotoById(int id)
        {
            return photoRepository.GetById(id);
        }
        public Photo AddPhoto(string photoPath, int postId)
        {
            var photo = new Photo
            {
                Picture = photoPath,
                PostId = postId
            };
            return photoRepository.Add(photo);
        }
        public Photo UpdatePhoto(Photo photoToUpdate)
        {
            return photoRepository.Update(photoToUpdate);
        }
        public bool RemovePhoto(int id)
        {
            return photoRepository.Remove(id);
        }
        public bool CheckPhoto(int id)
        {
            return photoRepository.Exists(id);
        }
        public IEnumerable<Photo> GetPhotoByPostId (int id)
        {
            return photoRepository.GetByPost(id);
        }

        public IEnumerable<Photo> GetPhotosByPostIds(IEnumerable<int> postsIds)
        {
            return photoRepository.GetByPostIds(postsIds);
        }
    }
}
