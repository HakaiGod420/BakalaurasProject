using DataLayer.Models;
using DataLayer.Repositories.Reviews;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ReviewService : IReviewService
    {
        public readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
           _reviewRepository = reviewRepository;
        }

        public async Task CreateReview(CreateReviewDto review, int userId)
        {
            var existingReview = await _reviewRepository.GetReview(userId,review.BoardGameId);

            if(existingReview != null)
            {
                existingReview.Rating = review.Rating;
                existingReview.Comment = review.Comment;
                await _reviewRepository.UpdateReview(existingReview);
                return;
            }

            var newReview = new ReviewEntity
            {
                WriterId = userId,
                WriteDate = DateTime.Now,
                IsBlocked = false,
                Rating = review.Rating,
                Comment = review.Comment,
                SelectedBoardGameId = review.BoardGameId,
            };

           await _reviewRepository.CreateReview(newReview);
        }

        public async Task<OldReview?> GetOldReview(int userId, int boardGameId)
        {
            return await _reviewRepository.GetOldReview(userId, boardGameId);
        }

        public async Task<List<ReviewView>> GetReviews(int boardGameId)
        {
            return await _reviewRepository.GetAllReviews(boardGameId);
        }
    }
}
