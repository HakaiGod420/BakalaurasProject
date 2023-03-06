using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Image
{
    public interface IImageRepository
    {
        public Task<ImageEntity> AddImage(ImageEntity image);
    }
}
