import { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { BsFillArrowRightCircleFill } from "react-icons/bs";
import { useParams } from "react-router-dom";
import { getGameBoardsCreatedByUser } from "../../services/api/GameBoardService";
import { SERVER_API } from "../../services/constants/ClienConstants";
import { UserCreatedGameBoard } from "../../services/types/TabletopGame";

const UserCreatedTableTopGames = () => {
  const params = useParams();
  const [currentPage, setCurrentPage] = useState(1);
  const [userCreatedGames, setUserCreatedGames] = useState<
    UserCreatedGameBoard[]
  >([]);
  const itemsPerPage = 5;
  const [totalPages, setTotalPages] = useState(1);

  const paginate = async (pageNumber: number) => {
    if (!params.username) return;
    console.log(pageNumber);
    const response = await getGameBoardsCreatedByUser(
      params.username,
      pageNumber - 1,
      itemsPerPage
    ).catch((error: AxiosError) => {
      return;
    });

    setUserCreatedGames(response?.GameBoards as UserCreatedGameBoard[]);
    setCurrentPage(pageNumber);
  };

  useEffect(() => {
    const getUserCreatedGames = async () => {
      if (!params.username) return;
      const response = await getGameBoardsCreatedByUser(
        params.username,
        0,
        itemsPerPage
      ).catch((error: AxiosError) => {
        return;
      });

      console.log(response);
      setUserCreatedGames(response?.GameBoards as UserCreatedGameBoard[]);
      const totalPages = Math.ceil(
        response ? response.TotalCount / itemsPerPage : 1
      );
      setTotalPages(totalPages);
    };
    getUserCreatedGames();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div className="flex flex-col items-center justify-center p-10 bg-white text-gray-800">
      {userCreatedGames.length > 0 ? (
        <div className=" w-[50%] px-4">
          <h2 className="text-2xl font-bold mb-4">Created Tabletop Games</h2>
          <table className="w-full border-collapse">
            <thead>
              <tr></tr>
              <tr></tr>
              <tr></tr>
            </thead>

            <tbody>
              {userCreatedGames.map((game) => (
                <tr key={game.GameBoardId}>
                  <td className="p-3 border-b border-gray-200 ">
                    <img
                      className="w-20 h-20 object-cover rounded-md"
                      src={SERVER_API + "/" + game.ImageUrl}
                      alt={game.Title}
                    />
                  </td>
                  <td className="p-3 border-b border-gray-200">
                    <a href={"/gameboards/" + game.GameBoardId}>
                      <span className="font-medium text-gray-800 text-[40px]">
                        {game.Title}
                      </span>
                    </a>
                  </td>
                  <td>
                    <button className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-md">
                      <a href={"/gameboards/" + game.GameBoardId}>
                        <BsFillArrowRightCircleFill />
                      </a>
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          <ul className="flex justify-center mt-4 w-full mb-4">
            {Array.from({ length: totalPages }, (_, i) => i + 1).map((page) => (
              <li key={page}>
                <button
                  className={`${
                    page === currentPage
                      ? "bg-green-500 text-white"
                      : "bg-gray-200 text-gray-700"
                  } py-2 px-4 mx-1 rounded`}
                  onClick={() => paginate(page)}
                >
                  {page}
                </button>
              </li>
            ))}
          </ul>
        </div>
      ) : (
        <div className="text-[25px]">
          User dont have any created tabletop games
        </div>
      )}
    </div>
  );
};

export default UserCreatedTableTopGames;
