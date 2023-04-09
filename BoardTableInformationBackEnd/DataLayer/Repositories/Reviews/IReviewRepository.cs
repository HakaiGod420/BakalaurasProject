using DataLayer.Models;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Reviews
{
    public interface IReviewRepository
    {
        public Task CreateReview(ReviewEntity review);
        public Task< List<ReviewView>> GetAllReviews(int boardGameId);
    }
}
