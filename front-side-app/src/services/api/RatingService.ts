import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import { Rating } from "../types/Rating";

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
