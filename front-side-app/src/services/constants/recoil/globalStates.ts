import { atom } from "recoil";

export const validTokenAtom = atom({
  key: "validToken", // unique ID (with respect to other atoms/selectors)
  default: false, // default value (aka initial value)
});

export const isAdminAtom = atom({
  key: "isAdmin",
  default: false,
});

export const activeInvitations = atom({
  key: "activeInvitations",
  default: 0,
});

export const userName = atom<string>({
  key: "userName",
  default: "",
});
