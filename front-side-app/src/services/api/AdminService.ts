import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import { TableTopGamesForReviewResponse } from "../types/TabletopGame";

export async function getBoardGameListForReview(startIndex: number, endIndex: number) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<TableTopGamesForReviewResponse>(
      SERVER_API + "/api/admin/getBoardForReview",
      {
        params: { StartIndex: startIndex, EndIndex: endIndex },
      }
    )
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}
