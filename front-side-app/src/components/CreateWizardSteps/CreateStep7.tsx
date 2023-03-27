import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  publishTabletopGame: () => void;
}

function CreateStep7({
  stepNumber,
  setStepNumber,
  publishTabletopGame,
}: Props) {
  const { handleStep, previousStep, nextStep } = useWizard();

  const inputHandlerNext = () => {
    publishTabletopGame();
  };

  const inputHandlerSkip = () => {};

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">Finish</h1>
        <p className="text-center p-2">
          Congratulations, you have completed all the required fields in the
          form! Please take a moment to review your submission and make any
          necessary corrections. Once you are certain that all information is
          accurate and complete, you may submit the form. Our team will review
          your submission and, if everything is correct, your tabletop game will
          be approved and showcased on our main website. Please note that the
          approval process may take some time, and we appreciate your patience
          while we review your submission
        </p>
        <div className="mb-5">
          <div className=" font-bold flex justify-center">
            <p className="text-[25px]">
              Are you sure you want to finish creating the board game?
            </p>
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
          Finish
        </button>
      </div>
    </div>
  );
}

export default CreateStep7;
