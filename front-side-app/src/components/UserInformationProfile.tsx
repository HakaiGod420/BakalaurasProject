import React, { useState } from "react";

const UserInformationProfile: React.FC = () => {
  const username = "John Smith";
  const registrationTime = "March 1, 2022";
  const lastLoginTime = "March 28, 2023";
  const role = "Admin";
  const profilePicture = "https://picsum.photos/200";
  const createdInvitations = 5;
  const createdTabletopGames = 3;
  const userState = "Active";

  // Define a class for the user state tag based on the user's state
  const userStateTagClass =
    userState === "Active" ? "bg-green-500" : "bg-red-500";

  // State variable to keep track of the active tab
  const [activeTab, setActiveTab] = useState("board-games");

  return (
    <div className="flex flex-col items-center justify-center p-10 bg-white text-gray-800">
      <div className="w-full max-w-3xl p-4 rounded-lg shadow-lg">
        <div className="flex items-center space-x-8 mb-8">
          <img
            src={profilePicture}
            alt="Profile picture"
            className="w-40 h-40 rounded-full border-4 border-gray-300 shadow-md"
          />
          <div>
            <h1 className="text-4xl font-bold">{username}</h1>
            <p className="text-xl">{`Role: ${role}`}</p>
            <p className="text-xl">{`Invitations created: ${createdInvitations}`}</p>
            <p className="text-xl">{`Tabletop games created: ${createdTabletopGames}`}</p>
            <span
              className={`inline-block px-3 py-1 rounded-full text-white text-sm font-semibold ${userStateTagClass}`}
            >
              {userState}
            </span>
          </div>
        </div>
        <div className="flex mb-4">
          <button
            className={`mr-2 py-2 px-4 rounded-lg font-semibold ${
              activeTab === "board-games"
                ? "bg-gray-300"
                : "bg-gray-200 hover:bg-gray-300"
            }`}
            onClick={() => setActiveTab("board-games")}
          >
            Created Board Games
          </button>
          <button
            className={`py-2 px-4 rounded-lg font-semibold ${
              activeTab === "invitations"
                ? "bg-gray-300"
                : "bg-gray-200 hover:bg-gray-300"
            }`}
            onClick={() => setActiveTab("invitations")}
          >
            Active Invitations
          </button>
        </div>
        {activeTab === "board-games" && (
          <ul className="list-disc pl-8">
            <li>Board game 1</li>
            <li>Board game 2</li>
            <li>Board game 3</li>
          </ul>
        )}
        {activeTab === "invitations" && (
          <ul className="list-disc pl-8">
            <li>Invitation 1</li>
            <li>Invitation 2</li>
            <li>Invitation 3</li>
          </ul>
        )}
        <table className="w-full text-left mt-4">
          <tbody>
            <tr>
              <td className="font-bold pr-4 text-xl">Registered on:</td>

              <td className="text-xl">{registrationTime}</td>
            </tr>
            <tr>
              <td className="font-bold pr-4 text-xl">Last login:</td>
              <td className="text-xl">{lastLoginTime}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default UserInformationProfile;
