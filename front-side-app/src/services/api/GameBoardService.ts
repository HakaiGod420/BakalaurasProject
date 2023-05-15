import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import {
  CategoriesAndTypes,
  Filter,
  SingleTabletopGame,
  TableTopGameCardsResponse,
  TabletopGameCreation,
  UserCreatedGameBoardsReposne,
} from "../types/TabletopGame";

export const postTableTopGame = async (
  tableboardgames: TabletopGameCreation
) => {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.post["Authorization"] = `Bearer ${token.token}`;

  await axios
    .post(SERVER_API + "/api/gameboard/create", tableboardgames, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    })
    .then((res) => {
      return res;
    })
    .catch((error) => {
      return Promise.reject(error);
    });
};

export const postImages = async (formData: FormData) => {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.post["Authorization"] = `Bearer ${token.token}`;

  await axios
    .post(SERVER_API + "/api/upload/imagePost", formData, {
      headers: { "Content-Type": "multipart/form-data" },
    })
    .then((res) => {
      return res;
    })
    .catch((error) => {
      return Promise.reject(error);
    });
};

export const postFiles = async (formData: FormData) => {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.post["Authorization"] = `Bearer ${token.token}`;

  await axios
    .post(SERVER_API + "/api/upload/filePost", formData, {
      headers: { "Content-Type": "multipart/form-data" },
    })
    .then((res) => {
      return res;
    })
    .catch((error) => {
      return Promise.reject(error);
    });
};

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

export async function checkIfGameBoardExist(gameboardName: string) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<boolean>(SERVER_API + "/api/gameboard/gameBoardExist", {
      params: { gameBoardName: gameboardName },
    })
    .catch((error) => {
      return Promise.reject(error);
    });

  return response?.data;
}

export async function getGameBoardsCreatedByUser(
  username: string,
  pageIndex: number,
  pageSize: number
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<UserCreatedGameBoardsReposne>(
      SERVER_API + "/api/gameboard/getUserCreatedGameBoards",
      {
        params: {
          username: username,
          pageIndex: pageIndex,
          pageSize: pageSize,
        },
      }
    )
    .catch((error) => {
      return Promise.reject(error);
    });

  return response?.data;
}
