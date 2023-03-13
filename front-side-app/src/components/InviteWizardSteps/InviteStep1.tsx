import axios from "axios";
import { useState } from "react";
import toast from "react-hot-toast";
import { useWizard } from "react-use-wizard";
import { SERVER_API } from "../../services/constants/ClienConstants";
import { ErrorBasic } from "../../services/types/Error";
import { SimpleTableTop } from "../../services/types/TabletopGame";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  title: string;
  setTitle: React.Dispatch<React.SetStateAction<string>>;
}

function CreateStep1({ stepNumber, setStepNumber, title, setTitle }: Props) {
  const { handleStep, nextStep } = useWizard();

  const [error, setError] = useState<boolean>(false);

  const getGame = async () => {
    const loading = toast.loading("Searching table top game");
    await axios
      .get(SERVER_API + "/api/gameboard/getBoardsSimple/", {
        params: { searchTerm: title },
      })
      .then((res) => {
        console.log(res.data);
        const Titles: SimpleTableTop[] = res.data;
        if (Titles.length === 0) {
          toast.error("Not such games was found", {
            id: loading,
          });
          setError(true);
          return;
        }
        toast.success("Game was founded", {
          id: loading,
        });
      })
      .catch((error) => {
        setError(true);
        const errorBasic: ErrorBasic = {
          status: error.response.status,
          code: error.code,
          message: error.message,
        };
        if (errorBasic.status === 400) {
        }
        toast.error("Error ocurred", {
          id: loading,
        });
      });
  };

  const inputHandlerNext = async () => {
    await getGame();
    if (error) {
      console.log(error);
      setStepNumber(stepNumber + 1);
      nextStep();
    }
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="p-5 uppercase font-bold text-[30px] text-center">
          Table top game Title
        </h1>
        <p className="p-5 text-center text-[20px]">
          Select the tabletop game for which you want to create an invitation
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
            onClick={() => inputHandlerNext()}
          >
            Next
          </button>
        </div>
      </div>
    </div>
  );
}

export default CreateStep1;
