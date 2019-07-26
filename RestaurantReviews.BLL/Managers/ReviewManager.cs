using RestaurantReviews.Models.Contracts;
using System;
using System.Linq;

namespace RestaurantReviews.BLL.Managers {
    public class ReviewManager {
        public decimal? CalculateScore(IReviewRepository reviewRepository, int id) {
            double? scoreAvg;
            try {
                scoreAvg = reviewRepository.Get(r => r.RestaurantId == id && r.IsActive == true && r.isConfirmed == true && r.Score != null).Average(s => s.Score);
            }
            catch (Exception) {
                return null;
            };

            if (scoreAvg.HasValue) {
                return Decimal.Round(Convert.ToDecimal(scoreAvg.Value), 1);
            }
            else {
                return null;
            }
        }
    }
}
