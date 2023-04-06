import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import {
  CategoriesAndTypes,
  SingleTabletopGame,
  TableTopGameCardsResponse,
} from "../types/TabletopGame";

export async function getBoardGameList(
  startIndex: number,
  endIndex: number,
  searchTerm: string | null
) {
  const response = await axios
    .get<TableTopGameCardsResponse>(
      SERVER_API + "/api/gameboard/getBoardCardItems",
      {
        params: {
          startIndex: startIndex,
          backIndex: endIndex,
          searchTerm: searchTerm,
        },
      }
    )
    .catch((error) => {
      console.log(error);
    });

  console.log(response?.data);
  return response?.data;
}

export async function getGameBoard(boardId: number) {
  const response = await axios
    .get<SingleTabletopGame>(SERVER_API + "/api/gameboard/getSingleBoard", {
      params: { boardId: boardId },
    })
    .catch((error) => {
      console.log(error);
    });
  return response?.data;
}

export async function getGameBoardTypesAndCategories() {
  const response = await axios
    .get<CategoriesAndTypes>(SERVER_API + "/api/selectList/typesAndCategories")
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}
