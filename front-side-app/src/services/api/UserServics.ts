import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import {
  AddressEdit,
  ChangedUserInformation,
  LoginData,
  NotificationSettings,
  RegisterData,
  UserInformation,
  UserSettings,
} from "../types/User";

export async function registerToSystem(crediantals: RegisterData) {
  await axios
    .post(SERVER_API + "/api/user/register", crediantals)
    .catch((error) => {
      Promise.reject(error);
      return error;
    });
}

export async function loginToSystem(crediantals: LoginData) {
  try {
    const res = await axios.post(SERVER_API + "/api/user/login", crediantals);
    localStorage.setItem("token", JSON.stringify({ token: res.data }));
    return res.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export async function login(username: string, password: string) {
  const crediantals: LoginData = {
    userName: username,
    password: password,
  };

  await axios
    .post(SERVER_API + "/api/user/login", crediantals)
    .then((res) => {
      return res.data;
    })
    .catch((error) => {
      Promise.reject(error);
      return error;
    });
}

export async function getUserSetting() {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<UserSettings>(SERVER_API + "/api/user/getUserSettings")
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}

export async function updateAddress(addressEdt: AddressEdit) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.put["Authorization"] = `Bearer ${token.token}`;

  await axios
    .put(SERVER_API + "/api/user/updateAddress", addressEdt)
    .catch((error) => {
      console.log(error);
    });
}

export async function updateNotifications(notifications: NotificationSettings) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.patch["Authorization"] = `Bearer ${token.token}`;

  await axios
    .patch(SERVER_API + "/api/user/updateNotifications", notifications)
    .catch((error) => {
      console.log(error);
    });
}

export async function getUserInformation(username: string) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.get["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .get<UserInformation>(SERVER_API + "/api/user/getUserInformation", {
      params: { userName: username },
    })
    .catch((error) => {
      console.log(error);
    });

  return response?.data;
}

export async function changeUserInformation(
  userInfoChanges: ChangedUserInformation
) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.put["Authorization"] = `Bearer ${token.token}`;

  const response = await axios.put(
    SERVER_API + "/api/user/changeUserInformation",
    userInfoChanges
  );

  return response;
}
