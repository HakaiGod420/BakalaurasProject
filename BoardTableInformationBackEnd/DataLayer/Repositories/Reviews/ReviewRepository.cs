using DataLayer.DBContext;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataBaseContext _dbContext;
        public ReviewRepository(DataBaseContext dataBaseContext)
        {
            _dbContext = dataBaseContext;
        }
        public async Task CreateReview(ReviewEntity review)
        {
            _dbContext.Reviews.Add(review);

            await _dbContext.SaveChangesAsync();
        }
    }
}
