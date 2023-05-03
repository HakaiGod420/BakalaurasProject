import { AxiosError } from "axios";
import toast from "react-hot-toast";
import { useWizard } from "react-use-wizard";
import { checkIfGameBoardExist } from "../../services/api/GameBoardService";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  title: string;
  setTitle: React.Dispatch<React.SetStateAction<string>>;
}

function CreateStep1({ stepNumber, setStepNumber, title, setTitle }: Props) {
  const { nextStep } = useWizard();

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const handleNextStep = async () => {
    const loading = toast.loading("Checking if game board exists...");
    var result = await checkIfGameBoardExist(title).catch(
      (error: AxiosError) => {
        if (error.response) {
          console.log(error.response.data);
          console.log(error.response.status);
          toast.error("Error occured", {
            id: loading,
          });

          return;
        }
      }
    );
    console.log(result);
    if (result) {
      toast.error("Game Board exist", {
        id: loading,
      });
      return;
    }

    toast.success("Game board doesnt exist", {
      id: loading,
    });

    inputHandlerNext();
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="p-5 uppercase font-bold text-[30px] text-center">
          Table top game Title
        </h1>
        <p className="p-5 text-center text-[20px]">
          Write a table top game title. The maximum length is 200 characters.
          The game must not have been created already.
        </p>
        <h1 className="p-5 uppercase font-bold text-[15px] text-center">
          Title
        </h1>
        <div>
          <div className="p-2 flex justify-center">
            <input
              onChange={(e) => setTitle(e.target.value)}
              value={title}
              type="text"
              placeholder="Tabletop game title"
              className="input input-bordered input-success w-full max-w-xs"
            />
          </div>
        </div>
        <div className="flex justify-center p-2 m-1">
          <button
            disabled={title === ""}
            className="btn m-2 min-w-[100px]"
            onClick={() => handleNextStep()}
          >
            Next
          </button>
        </div>
      </div>
    </div>
  );
}

export default CreateStep1;
