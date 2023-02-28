import jwt_decode from "jwt-decode";

export async function CheckJWTAndSession() {

    const token = JSON.parse(localStorage.getItem("token") || "false")

    if (token == false) {
        return false;
    }

    const decoded = jwt_decode(token.token)

    // @ts-ignore
    if (decoded.exp < Date.now() / 1000) {
        localStorage.clear();
        localStorage.clear();
        return false;
    }
    return true;
}

export async function CheckJWTIsAdmin() {

    const token = JSON.parse(localStorage.getItem("token") || "false")

    if (token == false) {
        return false;
    }

    const decoded = jwt_decode(token.token)

    // @ts-ignore
    if (decoded.exp < Date.now() / 1000) {
        localStorage.clear();
        return false;
    }
    // @ts-ignore
    if (decoded.IsAdmin == true) {
        // @ts-ignore
        return true;
    }
    return false;

}