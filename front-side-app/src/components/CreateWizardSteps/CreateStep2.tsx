import { useState } from "react";
import { useWizard } from "react-use-wizard";
import { TabletopGameCreation } from "../../services/types/TabletopGame";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  tableTopGame: TabletopGameCreation;
}

function CreateStep2({ stepNumber, setStepNumber, tableTopGame }: Props) {
  const { handleStep, previousStep, nextStep } = useWizard();

  const [playerAge, setPlayerAge] = useState<number>(0);
  const [playingTime, setPlayingTime] = useState<number>(0);
  const [playerCount, setPlayerCount] = useState<number>(0);

  const blockInvalidChar = (e: any) =>
    ["e", "E", "+", "-"].includes(e.key) && e.preventDefault();

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    tableTopGame.PlayingTime = playingTime;
    tableTopGame.PLayingAge = playerAge;
    console.log(tableTopGame);
    nextStep();
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  return (
    <div>
      <div>
        <h1 className=" uppercase font-bold text-[20px]">
          Players information
        </h1>
        <p className="p-2">
          Now you need to specify how old players should be to play this game
          and how long it takes on average to beat one round.
        </p>
        <div className="mb-5">
          <div className=" font-bold flex justify-center">
            <p>Player Age</p>
          </div>
          <div className="p-2 flex justify-center">
            <input
              min={0}
              onKeyDown={blockInvalidChar}
              onChange={(e) => setPlayerAge(parseInt(e.target.value))}
              type="number"
              placeholder="Player Age"
              className="input input-bordered input-success w-full max-w-xs"
            />
          </div>
        </div>
        <div>
          <div className=" font-bold flex justify-center">
            <p>Average playing time for one game</p>
          </div>
          <div className="p-2 flex justify-center">
            <input
              min={0}
              onKeyDown={blockInvalidChar}
              onChange={(e) => setPlayingTime(parseInt(e.target.value))}
              type="number"
              placeholder="Playing time"
              className="input input-bordered input-success w-full max-w-xs"
            />
          </div>
        </div>
        <div>
          <div className=" font-bold flex justify-center">
            <p>Players count</p>
          </div>
          <div className="p-2 flex justify-center">
            <input
              min={0}
              onKeyDown={blockInvalidChar}
              onChange={(e) => setPlayerCount(parseInt(e.target.value))}
              type="number"
              placeholder="Players count"
              className="input input-bordered input-success w-full max-w-xs"
            />
          </div>
        </div>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>

        <button
          disabled={playerAge <= 0 || playingTime <= 0}
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default CreateStep2;
