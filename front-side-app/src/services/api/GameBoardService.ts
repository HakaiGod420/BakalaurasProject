import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import { TableTopGameCard } from "../types/TabletopGame";

export async function getBoardGameList(startIndex: number, endIndex: number) {
  const response = await axios
    .get<TableTopGameCard[]>(SERVER_API + "/api/gameboard/getBoardCardItems", {
      params: { startIndex: startIndex, backIndex: endIndex },
    })
    .catch((error) => {
      console.log(error);
    });

  console.log(response?.data);
  return response?.data;
}
