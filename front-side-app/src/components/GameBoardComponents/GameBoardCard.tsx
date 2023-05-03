import dayjs from "dayjs";
import { useEffect, useState } from "react";
import { FaCalendarAlt, FaStar, FaUser } from "react-icons/fa";
import { useParams } from "react-router-dom";
import { useRecoilState } from "recoil";
import NoImageFile from "../../assets/images/noImage.png";
import { getGameBoard } from "../../services/api/GameBoardService";
import { SERVER_API } from "../../services/constants/ClienConstants";
import { validTokenAtom } from "../../services/constants/recoil/globalStates";
import { SingleTabletopGame } from "../../services/types/TabletopGame";
import RatingModal from "../RatingModal";
import LoadingComponent from "../core/LoadingComponent";
import TableTopTabs from "./TableTopTabs";

const GameBoardCard: React.FC = () => {
  let { id } = useParams();

  const [isOpen, setIsOpen] = useState(false);
  const [validToken] = useRecoilState(validTokenAtom);

  const onClose = () => {
    setIsOpen(false);
  };
  const [gameBoard, setGameBoard] = useState<SingleTabletopGame | undefined>(
    undefined
  );

  useEffect(() => {
    const getGameBoardAsync = async () => {
      const data = await getGameBoard(Number(id));
      setGameBoard(data);
      console.log(data);
    };

    getGameBoardAsync();
  }, [setGameBoard]);

  return (
    <div>
      {isOpen && <RatingModal onClose={onClose} />}
      {gameBoard ? (
        <div>
          <div className="max-w-screen-xl mx-auto p-4 flex flex-col md:flex-row items-center md:items-start">
            <div className="h-48 md:h-auto w-full md:w-1/2 lg:w-1/3 bg-gray-100 relative mb-4 md:mb-0 md:mr-4">
              <img
                src={
                  gameBoard.Thumbnail_Location
                    ? SERVER_API + "/" + gameBoard?.Thumbnail_Location
                    : NoImageFile
                }
                alt={`Thumbnail`}
                className="object-cover w-full h-full rounded-lg  shadow-lg"
              />
            </div>
            <div className="w-full h-[306px]  md:w-1/2 lg:w-2/3 bg-white rounded-md shadow-xl overflow-hidden p-4 md:p-6 flex flex-col justify-between">
              <div>
                <h2 className="text-lg font-medium text-gray-800">
                  {gameBoard.Title}
                </h2>
                <div className="flex flex-wrap items-center mt-2">
                  <p className="text-sm font-medium text-gray-700 mr-2 mb-2 md:mb-0">
                    Categories:
                  </p>
                  <span className="text-sm font-medium text-gray-500">
                    {gameBoard.Categories.length <= 2 ? (
                      <p>{gameBoard.Categories.join(", ")}</p>
                    ) : (
                      <span>
                        {gameBoard.Categories.slice(0, 2).join(", ")} and
                        more...
                      </span>
                    )}
                  </span>
                  <span className="mx-2 text-gray-400">&bull;</span>
                  <p className="text-sm font-medium text-gray-700 mr-2 mb-2 md:mb-0">
                    Types:
                  </p>
                  <span className="text-sm font-medium text-gray-500">
                    {gameBoard.Types.length <= 2 ? (
                      <p>{gameBoard.Types.join(", ")}</p>
                    ) : (
                      <span>
                        {gameBoard.Types.slice(0, 2).join(", ")} and more...
                      </span>
                    )}
                  </span>
                </div>
                <div className="mt-2 flex flex-wrap items-center">
                  <FaCalendarAlt className="h-5 w-5 text-gray-400" />
                  <p className="ml-2 text-sm font-medium text-gray-500">
                    Created:{" "}
                    {dayjs(gameBoard.CreationTime).format("YYYY-MM-DD")}
                  </p>
                  <span className="mx-2 text-gray-400">&bull;</span>
                  <FaCalendarAlt className="h-5 w-5 text-gray-400" />
                  <p className="ml-2 text-sm font-medium text-gray-500">
                    Last updated:
                    {gameBoard.UpdateDate
                      ? dayjs(gameBoard.UpdateDate).format("YYYY-MM-DD")
                      : " Never"}
                  </p>
                </div>
                <div className="mt-2 flex flex-wrap items-center">
                  <FaUser className="h-5 w-5 text-gray-400" />
                  <p className="ml-2 text-sm font-medium text-gray-500">
                    Creator: {gameBoard.CreatorName}
                  </p>
                </div>
                <div className="mt-2 flex flex-wrap items-center">
                  <FaStar className="h-5 w-5 text-gray-400" />
                  <p className="ml-2 text-sm font-medium text-gray-500">
                    Rating: {gameBoard.Rating?.toFixed(2)}/5
                  </p>
                </div>
              </div>
              {validToken && (
                <div className="flex justify-between mt-4">
                  <button
                    onClick={() => setIsOpen(true)}
                    className="bg-gray-300 hover:bg-gray-400 text-gray-700 font-medium py-2 px-4 rounded"
                  >
                    Rate
                  </button>
                </div>
              )}
            </div>
          </div>
          <TableTopTabs gameBoard={gameBoard} />
        </div>
      ) : (
        <div className="p-[300px]">
          <LoadingComponent />
        </div>
      )}
    </div>
  );
};

export default GameBoardCard;
