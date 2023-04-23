import { useState } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  rules: string | undefined;
  setRules: React.Dispatch<React.SetStateAction<string | undefined>>;
}

function CreateStep4({ stepNumber, setStepNumber }: Props) {
  const { handleStep, previousStep, nextStep } = useWizard();

  const [rules, setRules] = useState<string>("");

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerSkip = () => {
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
        <h1 className="text-center uppercase font-bold text-[20px]">
          Tabletop game rules
        </h1>
        <p className="p-4 text-center">
          Please provide the rules for your tabletop game. While this step is
          optional, it can be helpful for others who may not be familiar with
          how to play your game.
        </p>
        <div className="mb-5">
          <div className=" p-2 font-bold flex justify-center">
            <p>Game Rules</p>
          </div>
          <div className="p-2 flex justify-center">
            <textarea
              value={rules}
              onChange={(e) => setRules(e.target.value)}
              className="input input-bordered input-success w-full max-w-2xs max-h-[400px] min-h-[160px]"
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

        {rules !== "" && rules !== undefined ? (
          <button
            disabled={rules.length <= 100}
            className="btn m-2 min-w-[100px]"
            onClick={() => inputHandlerNext()}
          >
            Next
          </button>
        ) : (
          <button
            className="btn m-2 min-w-[100px]"
            onClick={() => inputHandlerSkip()}
          >
            Skip
          </button>
        )}
      </div>
    </div>
  );
}

export default CreateStep4;
