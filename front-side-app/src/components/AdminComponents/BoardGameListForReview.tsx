import { useEffect, useState } from "react";
import { FaCheck, FaTimes } from "react-icons/fa";
import { getBoardGameListForReview } from "../../services/api/AdminService";
import { TableTopGameItemForReview } from "../../services/types/TabletopGame";
import LoadingComponent from "../core/LoadingComponent";
import SectionDivider from "../core/SectionDivider";

interface GameBoard {
  id: number;
  title: string;
  createDate: string;
  creatorName: string;
}

const BoardGameListForReview: React.FC = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const [gameBoardsPerPage] = useState(5);

  const [totalCount, setTotalCount] = useState<number | undefined>(0);

  const [gameBoardsForReview, setGameBoardsForReview] =
    useState<TableTopGameItemForReview[]>();

  // Example game board data

  // Change page
  const paginate = (pageNumber: number) => {
    setCurrentPage(pageNumber);
  };

  useEffect(() => {
    const fetchGameBoardsForReview = async () => {
      const response = await getBoardGameListForReview(0, 5);
      setGameBoardsForReview(response?.GameBoardsForReview);
      setTotalCount(response?.TotalCount);
      console.log(Math.ceil(totalCount ? totalCount : 0 / gameBoardsPerPage));
    };
    fetchGameBoardsForReview();
  }, [setTotalCount]);

  return (
    <div className="max-w-[1280px] mx-auto">
      {gameBoardsForReview ? (
        <div>
          <div className="pt-5">
            <SectionDivider label={"Game boards for review"} />
          </div>

          <div className="overflow-x-auto">
            <table className="w-full border-collapse text-left rounded-md overflow-hidden">
              <thead>
                <tr className="bg-gray-800 text-white">
                  <th className="p-3 font-medium">Game board ID</th>
                  <th className="p-3 font-medium">Game board title</th>
                  <th className="p-3 font-medium">Created date</th>
                  <th className="p-3 font-medium">Creator name</th>
                  <th className="p-3 font-medium">Actions</th>
                </tr>
              </thead>
              <tbody>
                {gameBoardsForReview?.map((gameBoard) => (
                  <tr
                    key={gameBoard.GameBoardId}
                    className="bg-gray-700 text-gray-200 hover:bg-gray-600"
                  >
                    <td className="p-3">{gameBoard.GameBoardId}</td>
                    <td className="p-3">{gameBoard.GameBoardName}</td>
                    <td className="p-3">{gameBoard.GameBoardCreateDate}</td>
                    <td className="p-3">{gameBoard.CreatorName}</td>
                    <td className="p-3 flex justify-between items-center">
                      <button className="p-2 bg-green-600 rounded-md text-white hover:bg-green-500 w-[100px]">
                        <a href={`/game-boards/${gameBoard.GameBoardId}`}>
                          View
                        </a>
                      </button>
                      <div className="flex items-center">
                        <button className="p-2 text-green-500">
                          <FaCheck />
                        </button>
                        <button className="p-2 text-red-500">
                          <FaTimes />
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
                    totalCount ? totalCount : 0 / gameBoardsPerPage
                  ),
                },
                (_, i) => {
                  if (
                    i === 0 ||
                    i ===
                      Math.ceil(
                        totalCount ? totalCount : 0 / gameBoardsPerPage
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
                          totalCount ? totalCount : 0 / gameBoardsPerPage
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
          <LoadingComponent />
        </div>
      )}
    </div>
  );
};
export default BoardGameListForReview;
