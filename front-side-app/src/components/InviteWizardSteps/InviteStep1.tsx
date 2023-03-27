import axios from "axios";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { useWizard } from "react-use-wizard";
import { useRecoilState } from "recoil";
import { SERVER_API } from "../../services/constants/ClienConstants";
import { selectedGameBoard } from "../../services/constants/recoil/gameboardStates";
import { ErrorBasic } from "../../services/types/Error";
import { SimpleTableTop } from "../../services/types/TabletopGame";
import ChooseGame from "../ChooseGame";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  title: string;
  setTitle: React.Dispatch<React.SetStateAction<string>>;
}

function CreateStep1({ stepNumber, setStepNumber, title, setTitle }: Props) {
  const { nextStep } = useWizard();

  const [error, setError] = useState<boolean>(true);

  const [games, setGame] = useState<SimpleTableTop[]>([]);

  const [selectedGame, setSelectedGame] = useRecoilState(selectedGameBoard);

  const getGame = async () => {
    const loading = toast.loading("Searching table top game");
    await axios
      .get(SERVER_API + "/api/gameboard/getBoardsSimple/", {
        params: { searchTerm: title },
      })
      .then((res) => {
        const Titles: SimpleTableTop[] = res.data;
        if (Titles.length === 0) {
          toast.error("Not such games was found", {
            id: loading,
          });
          setError(true);
          return;
        }
        setGame(res.data);
        setError(false);
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
        return;
      });
    return;
  };

  const inputHandlerNext = async () => {
    if (games.length === 0) {
      await getGame();
    } else if (selectedGame !== undefined) {
      nextStep();
      setStepNumber(stepNumber + 1);
    }
  };

  const inputHandlerPrevious = () => {
    setSelectedGame(undefined);
    setGame([]);
  };

  useEffect(() => {
    if (!error) {
      inputHandlerNext();
    }
  }, [error]);

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="p-5 uppercase font-bold text-[30px] text-center">
          Table top game Title
        </h1>
        <p className="p-5 text-center text-[20px]">
          Please choose the tabletop game for which you would like to create an
          invitation
        </p>
        <h1 className="p-5 uppercase font-bold text-[15px] text-center">
          Title
        </h1>
        {games.length !== 0 && (
          <div>
            <ChooseGame games={games} />
          </div>
        )}
        {games.length === 0 && selectedGame === undefined && (
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
        )}

        <div className="flex justify-center p-2 m-1">
          {selectedGame !== undefined && (
            <button
              className="btn m-2 min-w-[100px]"
              onClick={inputHandlerPrevious}
            >
              Return
            </button>
          )}
          <button
            disabled={title === ""}
            className="btn m-2 min-w-[100px]"
            onClick={inputHandlerNext}
          >
            Next
          </button>
        </div>
      </div>
    </div>
  );
}

export default CreateStep1;
