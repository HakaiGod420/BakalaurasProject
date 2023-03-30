import dayjs from "dayjs";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useRecoilState } from "recoil";
import { getUserInformation } from "../services/api/UserServics";
import { userName } from "../services/constants/recoil/globalStates";
import { UserInformation } from "../services/types/User";
import LoadingComponent from "./core/LoadingComponent";
import InviteToGameFromProfile from "./InviteToGame";

const UserInformationProfile: React.FC = () => {
  const params = useParams();
  const [username] = useRecoilState(userName);
  dayjs().format();

  const [userInformation, setUserInformation] = useState<
    UserInformation | undefined
  >(undefined);

  const [modalOpen, setModalOpen] = useState(false);

  const profilePicture = "https://picsum.photos/200";

  // Define a class for the user state tag based on the user's state
  const userStateTagClass =
    userInformation?.State === "Active" ? "bg-green-500" : "bg-red-500";

  // Handler function for the "Invite to Game" button
  const handleInviteToGame = () => {
    // Implement your logic for inviting the user to a game here
    console.log("Invite user to game clicked");
    setModalOpen(true);
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
        <LoadingComponent />
      )}
      <InviteToGameFromProfile
        modalOpen={modalOpen}
        setModalOpen={setModalOpen}
      />
    </div>
  );
};

export default UserInformationProfile;
