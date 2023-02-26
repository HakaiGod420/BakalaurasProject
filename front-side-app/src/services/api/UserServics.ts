import axios from "axios";
import { SERVER_API } from "../constants/ClienConstants";
import { ErrorBasic } from "../types/Error";
import { LoginData } from "../types/User";

export default async function login(username:string, password:string) {
    const crediantals: LoginData = {
      userName: username,
      password: password,
    };

    await axios
      .post(SERVER_API + "/api/user/login", crediantals)
      .then(res => {
        return res.data
      })
      .catch(error => {
        const errorBasic : ErrorBasic = {
            status: error.response.status,
            code: error.code,
            message: error.message
        }
        return error
      });
  };