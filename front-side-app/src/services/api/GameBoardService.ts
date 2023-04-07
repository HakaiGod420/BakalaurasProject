import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import {
  CategoriesAndTypes,
  Filter,
  SingleTabletopGame,
  TableTopGameCardsResponse,
} from "../types/TabletopGame";

export async function getBoardGameList(
  startIndex: number,
  endIndex: number,
  searchTerm: string | null,
  filter: Filter
) {
  console.log(filter.types.map((x) => parseInt(x.value)));

  const response = await axios
    .get<TableTopGameCardsResponse>(
      SERVER_API + "/api/gameboard/getBoardCardItems",
      {
        params: {
          startIndex: startIndex,
          backIndex: endIndex,
          searchTerm: searchTerm,
          Title: filter.title,
          Types: filter.types.map((x) => parseInt(x.value)),
          Rating: filter.rating,
          Categories: filter.categories.map((x) => parseInt(x.value)),
          CreationDate: filter.creationDate,
        },
        paramsSerializer: {
          indexes: null,
        },
      }
    )
    .catch((error) => {
      console.log(error);
    });

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
