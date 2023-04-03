import { motion } from "framer-motion";
import { useState } from "react";
import { FaStar } from "react-icons/fa";

type Review = {
  username: string;
  profileImage: string;
  reviewText: string;
  rating: number;
  written: string;
};

const reviews: Review[] = [
  {
    username: "John Smith",
    profileImage: "https://randomuser.me/api/portraits/men/1.jpg",
    reviewText:
      "I've played a lot of board games in my time, but this one stands out as one of the best. The gameplay is engaging and challenging, but not so complex that it's hard to learn. The artwork and components are top-notch. Overall, I would highly recommend this game to anyone who loves board games.",
    rating: 5,
    written: "2022-03-31",
  },
  {
    username: "Jane Doe",
    profileImage: "https://randomuser.me/api/portraits/women/2.jpg",
    reviewText:
      "I was excited to try this game, but unfortunately it didn't live up to my expectations. The gameplay felt slow and repetitive, and the artwork wasn't very engaging. I wouldn't recommend this game to others.",
    rating: 2,
    written: "2022-03-29",
  },
  {
    username: "Samuel Johnson",
    profileImage: "https://randomuser.me/api/portraits/men/3.jpg",
    reviewText:
      "This game is an absolute blast to play with friends. The rules are easy to learn, but there is still plenty of depth and strategy to keep things interesting. The artwork is beautiful and the components are high-quality. Highly recommend!",
    rating: 4,
    written: "2022-03-27",
  },
  // add more reviews here
];

const GameReviews = () => {
  const [activeIndex, setActiveIndex] = useState<number | null>(null);

  const handleReviewClick = (index: number) => {
    if (activeIndex === index) {
      setActiveIndex(null);
    } else {
      setActiveIndex(index);
    }
  };

  return (
    <div className="bg-gray-800 py-8">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <h2 className="text-2xl font-medium text-white">Game Reviews</h2>
        <div className="mt-4 space-y-4">
          {reviews.map((review, index) => (
            <div key={index} className="bg-gray-700 rounded-lg overflow-hidden">
              <div
                className="flex items-center p-4 cursor-pointer"
                onClick={() => handleReviewClick(index)}
              >
                <img
                  src={review.profileImage}
                  alt={review.username}
                  className="w-12 h-12 rounded-full mr-4"
                />
                <div className="flex-1">
                  <div className="text-white font-medium">
                    {review.username}
                  </div>
                  <div className="text-gray-400 text-sm">{review.written}</div>
                </div>
                <div className="flex items-center">
                  <div className="text-gray-400 mr-2">{review.rating}</div>
                  <div className="flex items-center">
                    {Array.from({ length: 5 }).map((_, i) => (
                      <FaStar
                        key={i}
                        className={`text-yellow-400 ${
                          i < review.rating ? "opacity-100" : "opacity-25"
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
                  <p className="text-white">{review.reviewText}</p>
                </motion.div>
              )}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default GameReviews;
