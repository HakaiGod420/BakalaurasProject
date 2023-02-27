import { atom } from "recoil";

export const validTokenAtom = atom({
    key: "validToken", // unique ID (with respect to other atoms/selectors)
    default: false, // default value (aka initial value)
  });