using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;

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
        public Photo AddPhoto(Photo photoToAdd)
        {
            return photoRepository.Add(photoToAdd);
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
    }
}
