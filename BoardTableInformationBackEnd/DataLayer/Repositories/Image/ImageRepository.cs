using DataLayer.DBContext;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Image
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataBaseContext _dbContext;

        public ImageRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<ImageEntity> AddImage(ImageEntity image)
        {
            _dbContext.Images.Add(image);
            await _dbContext.SaveChangesAsync();
            return image;
        }
    }
}
