import jwt_decode from "jwt-decode";
import { JWTDeCode } from "../types/Miscellaneous";

export async function CheckJWTAndSession() {
  const token = JSON.parse(localStorage.getItem("token") || "false");

  if (token === false) {
    return false;
  }

  const decoded: JWTDeCode = jwt_decode(token.token);

  // @ts-ignore
  if (decoded.exp < Date.now() / 1000) {
    localStorage.clear();
    localStorage.clear();
    return false;
  }
  return true;
}

export async function CheckJWTIsAdmin() {
  const token = JSON.parse(localStorage.getItem("token") || "false");
  console.log(token);
  if (token === false) {
    return false;
  }

  const decoded: JWTDeCode = jwt_decode(token.token);

  if (decoded.exp < Date.now() / 1000) {
    localStorage.clear();
    return false;
  }

  if (decoded.Role === "Admin") {
    return true;
  }
  return false;
}
