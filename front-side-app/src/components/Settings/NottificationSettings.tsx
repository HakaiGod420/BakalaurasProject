import React, { useState } from "react";
import toast from "react-hot-toast";
import { updateNotifications } from "../../services/api/UserServics";
import { NotificationSettings } from "../../services/types/User";

interface NotificationSettingsProps {
  isOpen: boolean;
  isEnabledInviteNotifications: boolean | undefined;
  onClose: () => void;
}

let notifications: NotificationSettings = {
  Notifications: [],
};

const Notifications: React.FC<NotificationSettingsProps> = ({
  isOpen,
  onClose,
  isEnabledInviteNotifications,
}) => {
  const [isGameInvitationEnabled, setIsGameInvitationEnabled] = useState(
    isEnabledInviteNotifications
  );

  const handleToggleGameInvitation = () => {
    setIsGameInvitationEnabled(!isGameInvitationEnabled);
    const invitationIndex = notifications.Notifications.findIndex(
      (notification) => notification.Title === "Invitation"
    );
    if (invitationIndex !== -1) {
      notifications.Notifications[invitationIndex].IsActive =
        !isGameInvitationEnabled;
    } else {
      notifications.Notifications.push({
        Title: "Invitation",
        IsActive: !isGameInvitationEnabled,
      });
    }
  };

  const saveNotificationsSettings = async () => {
    const loading = toast.loading("Updating notifications...");
    await updateNotifications(notifications);
    toast.success("Successfully updated", {
      id: loading,
    });
  };

  return (
    <>
      {isOpen && (
        <div className="fixed z-10 inset-0 overflow-y-auto">
          <div className="flex items-center justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
            <div
              className="fixed inset-0 transition-opacity"
              aria-hidden="true"
            >
              <div className="absolute inset-0 bg-black bg-opacity-70"></div>
            </div>

            <div
              className="inline-block align-bottom bg-white rounded-lg px-4 pt-5 pb-4 text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full sm:p-6"
              role="dialog"
              aria-modal="true"
              aria-labelledby="modal-headline"
            >
              <div className="flex justify-between items-center mb-6">
                <h2 className="text-2xl font-bold text-green-500">
                  Notification Settings
                </h2>
                <button onClick={onClose}>
                  <svg
                    className="w-6 h-6 text-gray-500 hover:text-gray-700 transition-colors duration-200"
                    fill="none"
                    stroke="currentColor"
                    viewBox="0 0 24 24"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth={2}
                      d="M6 18L18 6M6 6l12 12"
                    />
                  </svg>
                </button>
              </div>

              <div className="mb-6">
                <h3 className="text-lg font-medium mb-2 text-green-400">
                  Game Invitation Notifications
                </h3>
                <div className="flex items-center">
                  <button
                    type="button"
                    className={`${
                      isGameInvitationEnabled ? "bg-green-500" : "bg-gray-200"
                    } relative inline-flex items-center justify-center h-6 rounded-full w-11 transition-colors duration-200 focus:outline-none`}
                    onClick={handleToggleGameInvitation}
                  >
                    <span
                      className={`${
                        isGameInvitationEnabled
                          ? "translate-x-3"
                          : "translate-x-[-10px]"
                      } inline-block w-4 h-4 transform bg-white rounded-full transition-transform duration-200 ease-in-out`}
                    />
                  </button>
                  <span className="text-gray-800 ml-4">
                    Enable to receive game invitations
                  </span>
                </div>
              </div>

              <div className="flex justify-end">
                <button
                  disabled={notifications.Notifications.length === 0}
                  onClick={saveNotificationsSettings}
                  className="bg-white text-green-500 px-4 py-2 rounded-lg shadow-md font-medium mr-4 transition-all duration-200 hover:bg-green-500 hover:text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 disabled:bg-slate-300 disabled:text-white"
                >
                  Save
                </button>
                <button
                  onClick={onClose}
                  className="bg
                  gray-200 text-gray-800 px-4 py-2 rounded-lg shadow-md font-medium transition-all duration-200 hover:bg-red-500 hover:text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-gray-500 "
                >
                  Close
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default Notifications;
