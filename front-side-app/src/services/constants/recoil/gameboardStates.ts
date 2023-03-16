import { atom } from "recoil";
import { SimpleTableTop } from "../../types/TabletopGame";

export const selectedGameBoard = atom<SimpleTableTop | undefined>({
  key: "selectedGameBoard",
  default: undefined,
});
