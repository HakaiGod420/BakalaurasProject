import { useGoogleLogin } from "@react-oauth/google";
import React, { useState } from "react";
import { postEventInGoogleCalendar } from "../services/api/GoogleAPI";

interface Props {
  invitationId: number | undefined;
  onClose: () => void;
}

const EventAcceptedModal: React.FC<Props> = ({ onClose, invitationId }) => {
  const [isTracking, setIsTracking] = useState(false);

  const handleTracking = () => {
    setIsTracking(true);
  };

  const handleLoginSuccess = async (response: any) => {
    console.log(response.access_token);
    await postEventInGoogleCalendar(response.access_token);
  };

  const handleLoginFailure = () => {
    alert("Unable to connect to Google. Please try again later.");
  };

  const login = useGoogleLogin({
    onSuccess: (tokenResponse) => handleLoginSuccess(tokenResponse),
  });

  return (
    <div className="fixed z-10 inset-0 overflow-y-auto flex items-center justify-center">
      <div
        className="fixed inset-0 transition-opacity bg-gray-500 bg-opacity-75"
        aria-hidden="true"
      ></div>

      <div className="bg-gray-100 rounded-lg overflow-hidden shadow-xl transform transition-all sm:max-w-md w-full">
        <div className="px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
          <div className="sm:flex sm:items-center sm:justify-center">
            <div className="text-center w-full">
              <h3 className="text-lg leading-6 font-medium text-gray-900 mb-4">
                Do you wanna set it your google calendar?
              </h3>
              <div className="mt-5 sm:mt-4 sm:flex sm:flex-row-reverse">
                <button
                  onClick={() => login()}
                  type="submit"
                  className="w-1/2 inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-green-600 text-base font-medium text-white hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 sm:ml-3 sm:text-sm"
                >
                  Yes
                </button>
                <button
                  type="button"
                  onClick={onClose}
                  className="w-1/2 mt-3 inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 sm:mt-0 sm:mr-3 sm:text-sm"
                >
                  No
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventAcceptedModal;
