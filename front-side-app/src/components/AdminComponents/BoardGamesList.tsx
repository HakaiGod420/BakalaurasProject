import dayjs from "dayjs";
import { useEffect, useState } from "react";
import {
  getBoardGamesListForAdmin,
  updateGameBoardIsBlocked,
} from "../../services/api/AdminService";
import {
  TableTopGameForAdmin,
  TableTopGameIsBlocked,
} from "../../services/types/TabletopGame";
import LoadingComponent from "../core/LoadingComponent";
import SectionDivider from "../core/SectionDivider";
import GameEditFormModal from "./GameEditFormModal";

const BoardGameListAdmin: React.FC = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const [gameBoardsPerPage] = useState(5);

  const [totalCount, setTotalCount] = useState<number | undefined>(0);

  const [gameBoards, setGameBoards] = useState<TableTopGameForAdmin[]>();

  const [selectedId, setSelectedId] = useState<number | undefined>(0);
  const [openEditModal, setOpenEditModal] = useState<boolean>(false);
  // Example game board data

  const fetchGames = async (pageNumber: number) => {
    const response = await getBoardGamesListForAdmin(
      pageNumber - 1,
      gameBoardsPerPage
    );
    console.log(response);
    setGameBoards(response?.Boards);
    setTotalCount(response?.TotalCount);
  };

  // Change page
  const paginate = async (pageNumber: number) => {
    setCurrentPage(pageNumber);
    await fetchGames(pageNumber);
  };

  const handleStateChange = async (gameBoardId: number, isBlocked: boolean) => {
    const gameBoardNewState: TableTopGameIsBlocked = {
      isBlocked: !isBlocked,
      gameBoardId: gameBoardId,
    };
    const response = await updateGameBoardIsBlocked(gameBoardNewState);
    console.log(response);
    let obj = gameBoards?.find((o) => o.GameBoardId === gameBoardId);
    if (obj) {
      obj.IsBlocked = !obj.IsBlocked;
    }

    setGameBoards([...gameBoards!]);
  };

  const handleEditForm = (gameBoardId: number) => {
    setSelectedId(gameBoardId);
    setOpenEditModal(true);
  };

  useEffect(() => {
    fetchGames(1);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [setTotalCount]);

  return (
    <div className="max-w-[1280px] mx-auto">
      {gameBoards ? (
        <div>
          {totalCount && totalCount > 0 ? (
            <div>
              <div className="pt-5">
                <SectionDivider label={"All game boards"} />
              </div>

              <div className="overflow-x-auto">
                <table className="w-full border-collapse text-left rounded-md overflow-hidden">
                  <thead>
                    <tr className="bg-gray-800 text-white">
                      <th className="p-3 font-medium">Game board ID</th>
                      <th className="p-3 font-medium">Game board title</th>
                      <th className="p-3 font-medium">Created date</th>
                      <th className="p-3 font-medium">Creator name</th>
                      <th className="p-3 font-medium">State</th>
                      <th className="p-3 font-medium text-center">Actions</th>
                      <th className="p-3 font-medium">Blocked</th>
                    </tr>
                  </thead>
                  <tbody>
                    {gameBoards?.map((gameBoard) => (
                      <tr
                        key={gameBoard.GameBoardId}
                        className="bg-gray-700 text-gray-200 hover:bg-gray-600"
                      >
                        <td className="p-3">{gameBoard.GameBoardId}</td>
                        <td className="p-3">{gameBoard.Title}</td>
                        <td className="p-3">
                          {dayjs(gameBoard.GameBoardCreateDate).format(
                            "YYYY-MM-DD HH:mm"
                          )}
                        </td>
                        <td className="p-3">{gameBoard.CreatorName}</td>
                        <td className="p-3">{gameBoard.State}</td>
                        <td className="p-3 flex justify-between items-center">
                          <button className="p-2 bg-green-600 rounded-md text-white hover:bg-green-500 w-[100px] mr-[-80px]">
                            <a href={`/gameboards/${gameBoard.GameBoardId}`}>
                              View
                            </a>
                          </button>
                          <button
                            onClick={() =>
                              handleEditForm(gameBoard.GameBoardId)
                            }
                            className=" ml-10 p-2 bg-green-600 rounded-md text-white hover:bg-green-500 w-[100px]"
                          >
                            Edit
                          </button>
                        </td>
                        <td>
                          <div className="flex items-center">
                            <button
                              onClick={() =>
                                handleStateChange(
                                  gameBoard.GameBoardId,
                                  gameBoard.IsBlocked
                                )
                              }
                              type="button"
                              className={`${
                                gameBoard.IsBlocked
                                  ? "bg-green-500"
                                  : "bg-gray-200"
                              } relative inline-flex items-center justify-center h-6 rounded-full w-11 transition-colors duration-200 focus:outline-none`}
                            >
                              <span
                                className={`${
                                  gameBoard.IsBlocked
                                    ? "translate-x-3"
                                    : "translate-x-[-10px]"
                                } inline-block w-4 h-4 transform bg-white rounded-full transition-transform duration-200 ease-in-out`}
                              />
                            </button>
                          </div>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>

                {/* Pagination */}
                <div className="flex justify-center items-center mt-4 pb-5">
                  {Array.from(
                    {
                      length: Math.ceil(
                        (totalCount ? totalCount : 0) / gameBoardsPerPage
                      ),
                    },
                    (_, i) => {
                      if (
                        i === 0 ||
                        i ===
                          Math.ceil(
                            (totalCount ? totalCount : 0) / gameBoardsPerPage
                          ) -
                            1 ||
                        (i >= currentPage - 2 && i <= currentPage + 2)
                      ) {
                        return (
                          <button
                            key={i}
                            className={`px-3 py-1 rounded-md mx-2 ${
                              currentPage === i + 1
                                ? "bg-green-600 text-white"
                                : "bg-gray-600 text-gray-200 hover:bg-gray-500"
                            }`}
                            onClick={() => paginate(i + 1)}
                          >
                            {i + 1}
                          </button>
                        );
                      } else if (
                        (i === currentPage - 3 && currentPage > 3) ||
                        (i === currentPage + 3 &&
                          currentPage <
                            Math.ceil(
                              (totalCount ? totalCount : 0) / gameBoardsPerPage
                            ) -
                              4)
                      ) {
                        return <span key={i}>...</span>;
                      } else {
                        return null;
                      }
                    }
                  )}
                </div>
              </div>
            </div>
          ) : (
            <div className="flex justify-center items-center h-[500px]">
              For now where is now new games which need review
            </div>
          )}
        </div>
      ) : (
        <div className="flex justify-center items-center h-[500px]">
          <LoadingComponent />
        </div>
      )}
      {openEditModal && (
        <GameEditFormModal
          gameBoardId={selectedId}
          onClose={() => setOpenEditModal(false)}
        />
      )}
    </div>
  );
};
export default BoardGameListAdmin;
