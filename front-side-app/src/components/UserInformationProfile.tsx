import dayjs from "dayjs";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useRecoilState } from "recoil";
import { getUserInformation } from "../services/api/UserServics";
import { userName } from "../services/constants/recoil/globalStates";
import { UserInformation } from "../services/types/User";

const UserInformationProfile: React.FC = () => {
  const params = useParams();
  const [username] = useRecoilState(userName);
  dayjs().format();

  const [userInformation, setUserInformation] = useState<
    UserInformation | undefined
  >(undefined);

  const profilePicture = "https://picsum.photos/200";

  // Define a class for the user state tag based on the user's state
  const userStateTagClass =
    userInformation?.State === "Active" ? "bg-green-500" : "bg-red-500";

  // Handler function for the "Invite to Game" button
  const handleInviteToGame = () => {
    // Implement your logic for inviting the user to a game here
    console.log("Invite user to game clicked");
  };

  useEffect(() => {
    const getUserInformationAsync = async () => {
      if (!params.username) return;
      const response = await getUserInformation(params.username);
      console.log(response);
      setUserInformation(response);
    };
    getUserInformationAsync();
  }, [setUserInformation]);

  return (
    <div className="flex flex-col items-center justify-center p-10 bg-white text-gray-800">
      {userInformation !== undefined ? (
        <div className="w-full max-w-3xl p-4 rounded-lg shadow-lg">
          <div className="flex items-center space-x-8 mb-8">
            <img
              src={profilePicture}
              alt="Profile picture"
              className="w-40 h-40 rounded-full border-4 border-gray-300 shadow-md"
            />
            <div>
              <h1 className="text-4xl font-bold pb-5">
                {userInformation?.UserName}
              </h1>
              <p className="text-xl">{`Role: ${userInformation?.Role}`}</p>
              <p className="text-xl">{`Invitations created: ${userInformation?.InvititationsCreated}`}</p>
              <p className="text-xl">{`Tabletop games created: ${userInformation?.TableTopGamesCreated}`}</p>

              <span
                className={`inline-block px-3 py-1 mt-3 rounded-full text-white text-sm font-semibold ${userStateTagClass}`}
              >
                {userInformation?.State}
              </span>
            </div>
            <div>
              {username !== userInformation?.UserName && (
                <button
                  className="ml-4 float-right py-2 px-4 rounded-lg font-semibold bg-blue-500 text-white hover:bg-blue-600"
                  onClick={handleInviteToGame}
                >
                  Invite to Game
                </button>
              )}
            </div>
          </div>

          <table className="w-full text-left mt-4">
            <tbody>
              <tr>
                <td className="font-bold pr-4 text-xl">Registered on:</td>

                <td className="text-xl">
                  {dayjs(userInformation?.RegisteredOn).format(
                    "YYYY-MM-DD HH:mm:ss"
                  )}
                </td>
              </tr>
              <tr>
                <td className="font-bold pr-4 text-xl">Last login:</td>
                <td className="text-xl">
                  {dayjs(userInformation?.LastLogin).format(
                    "YYYY-MM-DD HH:mm:ss"
                  )}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      ) : (
        <div className="text-center">
          <div role="status">
            <svg
              aria-hidden="true"
              className="inline w-8 h-8 mr-2 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600"
              viewBox="0 0 100 101"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
                fill="currentColor"
              />
              <path
                d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
                fill="currentFill"
              />
            </svg>
            <span className="sr-only">Loading...</span>
          </div>
        </div>
      )}
    </div>
  );
};

export default UserInformationProfile;
