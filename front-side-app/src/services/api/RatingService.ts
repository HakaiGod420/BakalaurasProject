import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import { Rating, ReviewView } from "../types/Rating";

export async function postRating(rating: Rating) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.post["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .post(SERVER_API + "/api/review/review", rating)
    .catch((error) => {
      console.log(error);
    });

  return response;
}

export async function getReviews(boardGameId: number) {
  const response = await axios
    .get<ReviewView[]>(SERVER_API + "/api/review/reviews", {
      params: { boardGameId: boardGameId },
    })
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}
