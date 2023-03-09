import { useState } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
}

function CreateStep3({ stepNumber, setStepNumber }: Props) {
  const { handleStep, previousStep, nextStep } = useWizard();

  const [description, setDescription] = useState<string>("");

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
    <div>
      <div>
        <h1 className=" uppercase font-bold text-[20px]">
          Tabletop game description
        </h1>
        <p className="p-2">
          Write: A description for a tabletop game should be concise, clear and
          catchy. It should tell potential players what the game is about, how
          it works and why they should play it.
        </p>
        <div className="mb-5">
          <div className=" font-bold flex justify-center">
            <p>Game Description</p>
          </div>
          <div className="p-2 flex justify-center">
            <textarea
              onChange={(e) => setDescription(e.target.value)}
              className="input input-bordered input-success w-full max-w-2xs max-h-[400px] min-h-[100px]"
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
          disabled={description === "" || description.length <= 100}
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default CreateStep3;
