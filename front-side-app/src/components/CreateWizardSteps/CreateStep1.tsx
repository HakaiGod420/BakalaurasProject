import { useState } from "react";
import { useWizard } from "react-use-wizard";
import { TabletopGameCreation } from "../../services/types/TabletopGame";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  tableTopGame: TabletopGameCreation;
}

function CreateStep1({ stepNumber, setStepNumber, tableTopGame }: Props) {
  const { handleStep, nextStep } = useWizard();

  const [gameTitle, setGameTitle] = useState<string>("");

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    tableTopGame.Title = gameTitle;
    nextStep();
  };

  return (
    <div>
      <div>
        <h1 className=" uppercase font-bold text-[20px]">
          Table top game Title
        </h1>
        <p className="p-2">
          Write a table top game title. The maximum length is 200 characters.
          The game must not have been created already.
        </p>
        <div className="p-2 flex justify-center">
          <input
            onChange={(e) => setGameTitle(e.target.value)}
            type="text"
            placeholder="Tabletop game title"
            className="input input-bordered input-success w-full max-w-xs"
          />
        </div>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          disabled={gameTitle === ""}
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default CreateStep1;
