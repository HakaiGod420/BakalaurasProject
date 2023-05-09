import { useGoogleLogin } from "@react-oauth/google";
import React from "react";
import {
  GoogleEvent,
  MeetDate,
  Reminder,
  UserInvitation,
} from "../services/types/Invitation";

import dayjs from "dayjs";
import timezone from "dayjs/plugin/timezone";
import utc from "dayjs/plugin/utc";
import { useNavigate } from "react-router-dom";
import { postEventInGoogleCalendar } from "../services/api/GoogleAPI";

dayjs.extend(utc);
dayjs.extend(timezone);

interface Props {
  userInvitation: UserInvitation | undefined;
  onClose: () => void;
  onAccept: (invitationId: number) => Promise<void>;
}

const EventAcceptedModal: React.FC<Props> = ({
  onClose,
  userInvitation,
  onAccept,
}) => {

  const handleLoginSuccess = async (response: any) => {
    console.log(response.access_token);
    if (userInvitation !== undefined) {
      const meetStart: MeetDate = {
        dateTime: userInvitation.EventDate,
        timeZone: dayjs.tz.guess(),
      };

      const meetEnd: MeetDate = {
        dateTime: dayjs(userInvitation.EventDate).add(3, "hour").format(),
        timeZone: dayjs.tz.guess(),
      };

      const reminder: Reminder = {
        useDefault: false,
        overrides: [
          { method: "email", minutes: 60 },
          { method: "popup", minutes: 30 },
        ],
      };

      const event: GoogleEvent = {
        summary: "Tabletop game event " + userInvitation?.BoardGameTitle,
        location: userInvitation.EventFullLocation,
        start: meetStart,
        end: meetEnd,
        reminder: reminder,
      };

      await postEventInGoogleCalendar(response.access_token, event);
      await onAccept(userInvitation.InvitationId);
      onClose();

    }
  };

  const scopes = [
    "https://www.googleapis.com/auth/calendar.events",
    "https://www.googleapis.com/auth/calendar",
  ];

  const login = useGoogleLogin({
    onSuccess: (tokenResponse) => handleLoginSuccess(tokenResponse),
    scope: scopes.join(" "),
  });
  const handleLoginFailure = () => {
    alert("Unable to connect to Google. Please try again later.");
  };

  const onDecline = async () => {
    if (userInvitation !== undefined) {
      await onAccept(userInvitation.InvitationId);
    }
    onClose();
  };

  return (
    <div className="fixed z-10 inset-0 overflow-y-auto flex items-center justify-center">
      <div
        className="fixed inset-0 transition-opacity bg-gray-500 bg-opacity-75"
        aria-hidden="true"
      ></div>

      <div className="bg-gray-100 rounded-lg overflow-hidden shadow-xl transform transition-all sm:max-w-md w-full">
        <div className="float-right p-1">
          <button
            type="button"
            className="bg-gray-200 rounded-full p-2 hover:bg-gray-300 focus:outline-none focus:ring"
            onClick={onClose}
          >
            <svg
              className="h-5 w-5 text-gray-600"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M6 18L18 6M6 6l12 12"
              />
            </svg>
          </button>
        </div>
        <div className="flex justify-between px-4 py-4">
          <h3 className="text-lg leading-6 font-medium text-gray-900 text-center flex-grow">
            <p>Do you want to set it on your Google calendar?</p>
          </h3>
        </div>

        <div className="px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
          <div className="sm:flex sm:items-center sm:justify-center">
            <div className="text-center w-full">
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
                  onClick={onDecline}
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
