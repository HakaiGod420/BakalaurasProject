import { useState } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
}

function InviteStep2({ stepNumber, setStepNumber }: Props) {
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
          What kind of people your searching
        </h1>
        <p className="text-center p-2">
          Write the rules of the tabletop game you are creating now. You can
          skip this step, but others may not know how to play your game.
        </p>
        <div>
          <div className=" font-bold flex justify-center">
            <p>Parcipants count</p>
          </div>
          <div className="p-2 flex justify-center">
            <input
              type="number"
              placeholder="Max Parcinpants Number"
              className="input input-bordered input-success w-full max-w-xs"
            />
          </div>
        </div>
        <div>
          <div className=" font-bold flex justify-center">
            <p>Minimal parcipants age</p>
          </div>
          <div className="p-2 flex justify-center">
            <input
              type="number"
              placeholder="Minimal parcipants age"
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
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default InviteStep2;
