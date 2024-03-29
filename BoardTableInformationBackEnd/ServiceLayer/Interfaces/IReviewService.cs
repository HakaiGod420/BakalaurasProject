﻿using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IReviewService
    {
        public Task CreateReview(CreateReviewDto review, int userId);
        public Task<List<ReviewView>> GetReviews(int boardGameId);
        public Task<OldReview?> GetOldReview(int userId, int boardGameId);
    }
}
