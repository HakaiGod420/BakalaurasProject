using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
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
            if(review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            _dbContext.Reviews.Add(review);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ReviewView>> GetAllReviews(int boardGameId)
        {
            var result = await _dbContext.Reviews
                .Include(x => x.Writer)
                .Where(x => x.SelectedBoardGameId == boardGameId && !x.IsBlocked)
                .Select(x => new ReviewView
                {
                    ReviewId = x.ReviewId,
                    ProfileImage = null,
                    Username = x.Writer.UserName,
                    CreatorId = x.WriterId,
                    ReviewText = x.Comment,
                    Rating = x.Rating,
                    Written = x.WriteDate
                })
                .ToListAsync();

            return result;
        }
    }
}
