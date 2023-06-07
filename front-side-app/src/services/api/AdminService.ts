import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import {
  GalleryForEdit,
  TableTopGameIsBlocked,
  TableTopGameListForAdminResponse,
  TableTopGamesForReviewResponse,
  TabletopGameAproval,
} from "../types/TabletopGame";

export async function getBoardGameListForReview(
  pageIndex: number,
  pageSize: number
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<TableTopGamesForReviewResponse>(
      SERVER_API + "/api/admin/getBoardForReview",
      {
        params: { PageIndex: pageIndex, PageSize: pageSize },
      }
    )
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}

export async function updateGameBoardState(
  tabletopGameAproval: TabletopGameAproval
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.patch["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .patch(SERVER_API + "/api/admin/changeGameBoardState", tabletopGameAproval)
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}

export async function updateGameBoardIsBlocked(
  gameBoardNewState: TableTopGameIsBlocked
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.patch["Authorization"] = `Bearer ${token.token}`;
  const response = await axios
    .patch(SERVER_API + "/api/admin/updateIsBlocked", gameBoardNewState)
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}

export async function getBoardGamesListForAdmin(
  pageIndex: number,
  pageSize: number
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<TableTopGameListForAdminResponse>(
      SERVER_API + "/api/admin/getGameBoardsForAdmin",
      {
        params: { pageIndex: pageIndex, pageSize: pageSize },
      }
    )
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}

export async function getGalleryItems(gameBoardId: number) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<GalleryForEdit>(SERVER_API + "/api/admin/getGalleryForEdit", {
      params: { gameBoardId: gameBoardId },
    })
    .catch((error) => {
      return Promise.reject(error);
    });

  return response?.data;
}

export async function deleteImage(imageId: number) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.delete["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .delete(SERVER_API + "/api/admin/deleteImage", {
      params: { imageId: imageId },
    })
    .catch((error) => {
      return Promise.reject(error);
    });

  return response?.data;
}
