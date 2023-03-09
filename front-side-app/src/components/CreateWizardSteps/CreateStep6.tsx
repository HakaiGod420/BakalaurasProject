import { useState } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
}

function CreateStep6({ stepNumber, setStepNumber }: Props) {
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
    <div>
      <div>
        <h1 className=" uppercase font-bold text-[20px]">Images</h1>
        <p className="p-2">
          Write the rules of the tabletop game you are creating now. You can
          skip this step, but others may not know how to play your game.
        </p>
        <div className="mb-5">
          <div className=" font-bold flex justify-center">
            <p>Upload Additional files</p>
          </div>
          <div className="flex justify-center mt-5">
            <input
              multiple={false}
              type="file"
              accept="image/png, image/jpeg, image/jpg"
              className="file-input file-input-bordered file-input-md w-full max-w-xs"
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

        {rules !== "" ? (
          <button
            disabled={rules.length <= 100}
            className="btn m-2 min-w-[10%]"
            onClick={() => inputHandlerNext()}
          >
            Next
          </button>
        ) : (
          <button
            className="btn m-2 min-w-[10%]"
            onClick={() => inputHandlerSkip()}
          >
            Skip
          </button>
        )}
      </div>
    </div>
  );
}

export default CreateStep6;
