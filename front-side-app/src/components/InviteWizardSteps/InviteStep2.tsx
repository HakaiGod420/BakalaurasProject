import { Dispatch, SetStateAction, useState } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  parcipantsCount: number | undefined;
  setParcipantsCount: Dispatch<SetStateAction<number | undefined>>;
  minimalParcipantsAge: number | undefined;
  setMinimalParcipantsAge: Dispatch<SetStateAction<number | undefined>>;
}

function InviteStep2({
  stepNumber,
  setStepNumber,
  setParcipantsCount,
  parcipantsCount,
  minimalParcipantsAge,
  setMinimalParcipantsAge,
}: Props) {
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
          To ensure that your table top game event is enjoyable and suitable for
          everyone, please enter the maximum number of participants you can
          accommodate and the minimum age requirement for your event. You can
          change these settings at any time before the event starts.
        </p>
        <div>
          <div className=" font-bold flex justify-center">
            <p>Participants count</p>
          </div>
          <div className="p-2 flex justify-center">
            <input
              onChange={(e) => setParcipantsCount(Number(e.target.value))}
              value={parcipantsCount}
              type="number"
              placeholder="Max Parcinpants Number"
              className="input input-bordered input-success w-full max-w-xs"
            />
          </div>
        </div>
        <div>
          <div className=" font-bold flex justify-center">
            <p>Minimal participants age</p>
          </div>
          <div className="p-2 flex justify-center">
            <input
              type="number"
              onChange={(e) => setMinimalParcipantsAge(Number(e.target.value))}
              value={minimalParcipantsAge}
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
          disabled={
            parcipantsCount === undefined ||
            parcipantsCount === 0 ||
            minimalParcipantsAge === 0 ||
            minimalParcipantsAge === undefined
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

export default InviteStep2;
