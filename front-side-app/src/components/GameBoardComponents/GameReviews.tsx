import dayjs from "dayjs";
import { motion } from "framer-motion";
import { useEffect, useState } from "react";
import { FaStar } from "react-icons/fa";
import { useParams } from "react-router-dom";
import { getReviews } from "../../services/api/RatingService";
import { ReviewView } from "../../services/types/Rating";

const GameReviews = () => {
  let { id } = useParams();

  const [reviewsViews, setReviewsViews] = useState<ReviewView[] | undefined>(
    []
  );

  const [activeIndex, setActiveIndex] = useState<number | null>(null);

  const handleReviewClick = (index: number) => {
    if (activeIndex === index) {
      setActiveIndex(null);
    } else {
      setActiveIndex(index);
    }
  };

  useEffect(() => {
    const getReviewsFromBackend = async () => {
      const reponse = await getReviews(Number(id));
      setReviewsViews(reponse);
    };
    getReviewsFromBackend();
  }, []);

  return (
    <div className="bg-gray-800 py-8">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <h2 className="text-2xl font-medium text-white">Game Reviews</h2>
        <div className="mt-4 space-y-4">
          {reviewsViews?.map((review, index) => (
            <div key={index} className="bg-gray-700 rounded-lg overflow-hidden">
              <div
                className="flex items-center p-4 cursor-pointer"
                onClick={() => handleReviewClick(index)}
              >
                <img
                  src={
                    review.ProfileImage
                      ? review.ProfileImage
                      : require("../../assets/images/profileImageDefault.png")
                  }
                  alt={review.Username}
                  className="w-12 h-12 rounded-full mr-4"
                />
                <div className="flex-1">
                  <div className="text-white font-medium">
                    {review.Username}
                  </div>
                  <div className="text-gray-400 text-sm">
                    {dayjs(review.Written).format("YYYY-MM-DD HH:mm")}
                  </div>
                </div>
                <div className="flex items-center">
                  <div className="text-gray-400 mr-2">{review.Rating}</div>
                  <div className="flex items-center">
                    {Array.from({ length: 5 }).map((_, i) => (
                      <FaStar
                        key={i}
                        className={`text-yellow-400 ${
                          i < review.Rating ? "opacity-100" : "opacity-25"
                        }`}
                      />
                    ))}
                  </div>
                </div>
              </div>
              {activeIndex === index && (
                <motion.div
                  initial={{ opacity: 0, height: 0 }}
                  animate={{ opacity: 1, height: "auto" }}
                  exit={{ opacity: 0, height: 0 }}
                  transition={{ duration: 0.3 }}
                  className="p-4 border-t border-gray-600"
                >
                  <p className="text-white">{review.ReviewText}</p>
                </motion.div>
              )}
            </div>
          ))}
          {reviewsViews?.length === 0 && (
            <p className="items-center text-center text-3xl opacity-50">
              Where is no review
            </p>
          )}
        </div>
      </div>
    </div>
  );
};

export default GameReviews;
