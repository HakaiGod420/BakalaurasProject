import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import {
  Invitation,
  InvitationStateChange,
  InvitationsList,
  Participant,
  ParticipationState,
  SentPersonalInvitation,
  UserInvitation,
} from "../types/Invitation";

export const postInvitation = async (invitation: Invitation) => {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.post["Authorization"] = `Bearer ${token.token}`;

  await axios
    .post(SERVER_API + "/api/gameboardinvitation/createInvitation", invitation)
    .then((res) => {
      return res;
    })
    .catch((error) => {
      return Promise.reject(error);
    });
};

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

export async function joinToInvitation(invitationId: number) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.post["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .post(SERVER_API + "/api/gameboardinvitation/joinInvitation", {
      SelectedActiveInvitation: invitationId,
    })
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

export async function getInvitationList(
  country: string,
  pageIndex: number,
  pageSize: number
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<InvitationsList>(
      SERVER_API + "/api/gameboardinvitation/getInvitationsByCountry",
      { params: { country: country, pageIndex: pageIndex, pageSize: pageSize } }
    )
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}

export async function getParticipants(invitationId: number) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<Participant[]>(SERVER_API + "/api/gameboardinvitation/parcipants", {
      params: { activeInvitationId: invitationId },
    })
    .catch((error) => {
      Promise.reject(error);
      return;
    });

  return response?.data;
}

export async function changeParticipantState(state: ParticipationState) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.patch["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .patch<boolean>(SERVER_API + "/api/gameboardinvitation/participants", state)
    .catch((error) => {
      Promise.reject(error);
      return;
    });

  console.log(response);
  return response?.data;
}
