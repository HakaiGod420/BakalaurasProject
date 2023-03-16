import { useEffect, useState } from "react";
import { useRecoilState } from "recoil";
import { selectedGameBoard } from "../services/constants/recoil/gameboardStates";
import { SimpleTableTop } from "../services/types/TabletopGame";

interface ChooseGameProps {
  games: SimpleTableTop[];
}

function ChooseGame({ games }: ChooseGameProps) {
  const [selectedIndex, setSelectedIndex] = useState<number>(1);

  const [selectedGame, setSelectedGame] = useRecoilState(selectedGameBoard);
  

  /*Fix this part of code*/
  const selectGameNext = () => {
    if (selectedGame === null) {
      setSelectedGame(games[0]);
      return;
    }
    if (games.length === selectedIndex) {
      console.log(selectedIndex);
      setSelectedGame(games[0]);
      setSelectedIndex(1);

      return;
    }
    console.log(selectedIndex);
    setSelectedIndex(selectedIndex + 1);
    setSelectedGame(games[selectedIndex]);

    return;
  };

  const selectGamePrevious = () => {
    if (selectedGame === null) {
      return;
    }
    if (0 === selectedIndex) {
      console.log(selectedIndex);
      setSelectedGame(games[selectedIndex]);
      setSelectedIndex(games.length - 1);
      return;
    }
    setSelectedIndex(selectedIndex - 1);
    console.log(selectedIndex);
    setSelectedGame(games[selectedIndex]);
  };

  useEffect(() => {
    selectGameNext();
  }, [games]);

  return (
    <div className="text-center bg-[#111827] border border-green-700 rounded-lg text-black h-[90px] flex items-center justify-around">
      {/*Need edit style here*/}
      <div className="grid gap-4 lg:grid-cols-3 sm:grid-cols-2">
        <div className="w-[50px] lg:w-[150px] flex justify-center items-center ">
          <button
            onClick={selectGamePrevious}
            className="btn lg:w-[100px] bg-[#323f5a]  border-green-700"
          >
            Prev
          </button>
        </div>
        <div className="w-[200px] hover:bg-slate-600 lg:w-[170px] flex justify-center items-center bg-[#273142] text-white m-2 rounded-md p-2">
          {selectedGame?.Title}
        </div>
        <div className="w-[50px] lg:w-[170px] flex justify-center items-center  ">
          <button
            onClick={selectGameNext}
            className="btn lg:w-[100px] bg-[#323f5a]  border-green-700"
          >
            Next
          </button>
        </div>
      </div>
    </div>
  );
}

export default ChooseGame;
