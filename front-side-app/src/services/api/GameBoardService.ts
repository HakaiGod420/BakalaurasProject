import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import { TableTopGameCardsResponse } from "../types/TabletopGame";

export async function getBoardGameList(startIndex: number, endIndex: number) {
  const response = await axios
    .get<TableTopGameCardsResponse>(
      SERVER_API + "/api/gameboard/getBoardCardItems",
      {
        params: { startIndex: startIndex, backIndex: endIndex },
      }
    )
    .catch((error) => {
      console.log(error);
    });

  console.log(response?.data);
  return response?.data;
}
