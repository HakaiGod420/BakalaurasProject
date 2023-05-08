import React, { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { BsStar, BsStarFill } from "react-icons/bs";
import { useParams } from "react-router-dom";
import { getOldReview, postRating } from "../services/api/RatingService";
import { Rating } from "../services/types/Rating";

interface Props {
  onClose: () => void;
  existingRating: number | undefined;
  ratingCount: number | undefined;
  setNewRating: React.Dispatch<React.SetStateAction<number | undefined>>;
}

const RatingModal: React.FC<Props> = ({
  onClose,
  existingRating,
  ratingCount,
  setNewRating,
}) => {
  const [rating, setRating] = useState<number>(0);
  const [comment, setComment] = useState<string | undefined>("");
  const [oldExist, setOldExist] = useState<boolean>(false);
  const [oldRating, setOldRating] = useState<number>(0);

  let { id } = useParams();

  const handleRatingClick = (newRating: number) => {
    setRating(newRating);
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const loading = toast.loading("Creating review...");
    const newRating: Rating = {
      BoardGameId: Number(id),
      Rating: rating,
      Comment: comment,
    };
    const response = await postRating(newRating);
    toast.success("Review posted", {
      id: loading,
    });
    if (!oldExist) {
      if (ratingCount === 0) {
        setNewRating(rating);
        return;
      }
      const sumOfRatings = existingRating! * ratingCount!;
      const newRatingCount = ratingCount! + 1;
      const newRatingValue = (sumOfRatings + rating) / newRatingCount;
      setNewRating(newRatingValue);
    }

    if (oldExist) {
      if (ratingCount === 0) {
        setNewRating(rating);
        onClose();
        return;
      }
      const sumOfRatings = existingRating! * ratingCount! - oldRating + rating;
      const newRatingValue = sumOfRatings / ratingCount!;
      setNewRating(newRatingValue);
    }

    onClose();
  };

  useEffect(() => {
    const getUserOldReview = async () => {
      const response = await getOldReview(Number(id));

      if (response != undefined) {
        setRating(response.Rating);
        setComment(response.Comment);
        setOldRating(response.Rating);
        setOldExist(true);
      }
    };
    getUserOldReview();
  }, []);

  return (
    <div className="fixed z-10 inset-0 overflow-y-auto flex items-center justify-center">
      <div
        className="fixed inset-0 transition-opacity bg-gray-500 bg-opacity-75"
        aria-hidden="true"
      ></div>

      <div className="bg-gray-100 rounded-lg overflow-hidden shadow-xl transform transition-all sm:max-w-md w-full">
        <div className="px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
          <div className="sm:flex sm:items-center sm:justify-center">
            <div className="text-center w-full">
              <h3 className="text-lg leading-6 font-medium text-gray-900 mb-4">
                Rate this game
              </h3>
              <div className="flex justify-center">
                {[1, 2, 3, 4, 5].map((value) => (
                  <StarButton
                    key={value}
                    value={value}
                    selected={rating >= value}
                    onClick={handleRatingClick}
                  />
                ))}
              </div>
              <form className="mt-4" onSubmit={handleSubmit}>
                <textarea
                  className="h-32 mt-4 block w-full rounded-md border-gray-300 shadow-sm bg-white p-1 text-black focus:border-blue-500 focus:ring-blue-500  sm:text-sm resize-none"
                  placeholder="Enter your comment here..."
                  value={comment}
                  onChange={(event) => setComment(event.target.value)}
                />
                <div className="mt-5 sm:mt-4 sm:flex sm:flex-row-reverse">
                  <button
                    disabled={rating === 0}
                    type="submit"
                    className="w-1/2 inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-green-600 text-base font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 sm:ml-3 sm:text-sm disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    Post
                  </button>
                  <button
                    type="button"
                    onClick={onClose}
                    className="w-1/2 mt-3 inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 sm:mt-0 sm:mr-3 sm:text-sm"
                  >
                    Cancel
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

interface StarButtonProps {
  value: number;
  selected: boolean;
  onClick: (value: number) => void;
}

const StarButton: React.FC<StarButtonProps> = ({
  value,
  selected,
  onClick,
}) => {
  const handleClick = () => {
    onClick(value);
  };

  return (
    <button
      type="button"
      className="focus:outline-none focus:ring-2 focus:ring-blue-500"
      onClick={handleClick}
    >
      {selected ? (
        <BsStarFill className="h-8 w-8 text-yellow-400" />
      ) : (
        <BsStar className="h-8 w-8 text-gray-400" />
      )}
      <span className="sr-only">{value} stars</span>
    </button>
  );
};

export default RatingModal;
