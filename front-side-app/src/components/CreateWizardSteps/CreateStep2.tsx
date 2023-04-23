import { useWizard } from "react-use-wizard";
import { TabletopGameCreation } from "../../services/types/TabletopGame";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  tableTopGame: TabletopGameCreation;

  playerAge: number | undefined;
  setAge: React.Dispatch<React.SetStateAction<number | undefined>>;
  playerCount: number | undefined;
  setPlayerCount: React.Dispatch<React.SetStateAction<number | undefined>>;
  averageTime: number | undefined;
  setAverageTime: React.Dispatch<React.SetStateAction<number | undefined>>;
}

function CreateStep2({
  stepNumber,
  setStepNumber,
  tableTopGame,
  playerAge,
  setAge,
  playerCount,
  setPlayerCount,
  averageTime,
  setAverageTime,
}: Props) {
  const { handleStep, previousStep, nextStep } = useWizard();

  const blockInvalidChar = (e: any) =>
    ["e", "E", "+", "-"].includes(e.key) && e.preventDefault();

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className=" p-2 text-center uppercase font-bold text-[20px]">
          Players information
        </h1>
        <p className="p-2 text-center">
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
              onChange={(e) => setAge(parseInt(e.target.value))}
              type="number"
              value={playerAge}
              placeholder="Player Age"
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
              value={playerCount}
              placeholder="Players count"
              className="input input-bordered input-success w-full max-w-xs"
            />
          </div>
        </div>
        <div>
          <div className=" font-bold flex justify-center mt-5">
            <p>Average playing time for one game</p>
          </div>
          <div className="p-2 flex justify-center">
            <input
              min={0}
              onKeyDown={blockInvalidChar}
              onChange={(e) => setAverageTime(parseInt(e.target.value))}
              value={averageTime}
              type="number"
              placeholder="Playing time"
              className="input input-bordered input-success w-full max-w-xs"
            />
          </div>
        </div>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>

        <button
          disabled={
            playerAge === undefined ||
            playerCount === undefined ||
            playerCount <= 0 ||
            playerAge <= 0
          }
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default CreateStep2;
