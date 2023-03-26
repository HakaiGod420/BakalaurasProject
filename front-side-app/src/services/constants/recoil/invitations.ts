import { atom } from "recoil";
import { UserInvitation } from "../../types/Invitation";

export const acceptedInvitations = atom<UserInvitation[]>({
  key: "acceptedInvitations",
  default: [],
});
