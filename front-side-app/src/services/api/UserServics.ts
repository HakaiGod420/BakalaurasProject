import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import { ErrorBasic } from "../types/Error";
import {
  AddressEdit,
  LoginData,
  NotificationSettings,
  UserSettings,
} from "../types/User";

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
      const errorBasic: ErrorBasic = {
        status: error.response.status,
        code: error.code,
        message: error.message,
      };
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

  const response = await axios
    .put(SERVER_API + "/api/user/updateAddress", addressEdt)
    .catch((error) => {
      console.log(error);
    });
}

export async function updateNotifications(notifications: NotificationSettings) {
  const token = JSON.parse(localStorage.getItem("token") ?? "{}");

  axios.defaults.headers.patch["Authorization"] = `Bearer ${token.token}`;

  const response = await axios
    .patch(SERVER_API + "/api/user/updateNotifications", notifications)
    .catch((error) => {
      console.log(error);
    });

  console.log(response);
}
