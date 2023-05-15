import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  description: string;
  setDescription: React.Dispatch<React.SetStateAction<string>>;
}

function CreateStep3({
  stepNumber,
  setStepNumber,
  description,
  setDescription,
}: Props) {
  const {previousStep, nextStep } = useWizard();

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
        <h1 className=" text-center uppercase font-bold text-[20px]">
          Tabletop game description
        </h1>
        <p className="p-2 text-center">
          Write: A description for a tabletop game should be concise, clear and
          catchy. It should tell potential players what the game is about, how
          it works and why they should play it. Minimal 100 characters.
        </p>
        <div className="mb-5 p-5">
          <div className=" font-bold flex justify-center">
            <p>Game Description</p>
          </div>
          <div className="p-2 flex justify-center">
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              className="input input-bordered input-success w-full max-w-2xs max-h-[400px] min-h-[160px]"
            />
          </div>
        </div>
      </div>
      <div className="flex justify-center m-1">
        <button
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>

        <button
          disabled={description === "" || description.length <= 100}
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default CreateStep3;
