import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import {
  InvitationStateChange,
  SentPersonalInvitation,
  UserInvitation,
} from "../types/Invitation";

export async function getActiveIntitations() {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<UserInvitation[]>(SERVER_API + "/api/gameboardinvitation/invitations")
    .catch((error) => {
      console.log(error);
    });

  if (response?.data) {
    return response.data;
  } else {
    return [];
  }
}

export async function getAcceptedInvitations() {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<UserInvitation[]>(
      SERVER_API + "/api/gameboardinvitation/activeInvitations"
    )
    .catch((error) => {
      console.log(error);
    });

  if (response?.data) {
    return response.data;
  } else {
    return [];
  }
}

export async function getCreatedInvitations() {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<UserInvitation[]>(
      SERVER_API + "/api/gameboardinvitation/createdInvitations"
    )
    .catch((error) => {
      console.log(error);
    });

  if (response?.data) {
    return response.data;
  } else {
    return [];
  }
}

export async function updateInvitationState(
  invistationState: InvitationStateChange
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.patch["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .patch(
      SERVER_API + "/api/gameboardinvitation/updateInvitationState",
      invistationState
    )
    .catch((error) => {
      console.log(error);
    });

  if (response?.data) {
    return response.data;
  } else {
    return [];
  }
}

export async function getActiveInvitationCount() {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<number>(SERVER_API + "/api/gameboardinvitation/activeInvitationCount")
    .catch((error) => {
      console.log(error);
    });

  if (response?.data) {
    return response.data;
  } else {
    return 0;
  }
}

export async function sentPersonalInvitation(
  invitation: SentPersonalInvitation
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.post["Authorization"] = `Bearer ${token.token}`;

  await axios
    .post(
      SERVER_API + "/api/gameboardinvitation/sentInvitationToUser",
      invitation
    )
    .catch((error) => {
      console.log(error);
    });
}
