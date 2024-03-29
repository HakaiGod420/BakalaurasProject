import dayjs from "dayjs";
import L from "leaflet";
import React, { useState } from "react";
import { BsArrowRightCircle } from "react-icons/bs";
import { MapContainer, Marker, TileLayer } from "react-leaflet";
import { Link } from "react-router-dom";
import marker from "../../assets/images/market.png";
import { UserInvitation } from "../../services/types/Invitation";
import ParcipantsModal from "../ParcipantsModal";

interface EventInvitationProps {
  invitation: UserInvitation;
  onAccept: (invitationId: number) => void;
  onReject: (invitationId: number) => void;
  itsInvitation: boolean;
  createdInvitation?: boolean;
}

const EventCard: React.FC<EventInvitationProps> = ({
  invitation,
  onAccept,
  onReject,
  itsInvitation,
  createdInvitation,
}) => {
  const [showMap, setShowMap] = useState(false);
  const [parcipantsOpen, setParcipantsOpen] = useState(false);
  const [acceptedParticipants, setAcceptedParticipants] = useState<number>(
    invitation.AcceptedCount
  );

  const toggleMap = () => {
    setShowMap(!showMap);
  };

  const icon = L.icon({ iconUrl: marker, iconSize: [40, 40] });

  return (
    <div className="lg:w-[400px] md:w-[300px] w-[400px] mx-auto bg-white border-2 border-green-500 shadow-lg rounded-lg overflow-hidden">
      <div className="sm:flex sm:items-center px-6 py-4">
        <div className="text-center sm:text-left sm:flex-grow">
          <div className="mb-4">
            <h2 className="text-2xl font-bold text-gray-900 flex">
              <Link
                to={"/"}
                className="flex items-center justify-center transition-all duration-200 hover:text-white hover:bg-green-500 rounded-full p-1"
              >
                <span className="text-gray-700 flex items-center justify-center">
                  {invitation.BoardGameTitle}
                </span>
                <BsArrowRightCircle className="ml-1 text-md text-black" />
              </Link>
            </h2>
            <p className="text-sm text-gray-600">
              Date: {dayjs(invitation.EventDate).format("YYYY-MM-DD HH:mm")}
            </p>
            <p className="text-sm text-gray-600">
              Location: {invitation.EventFullLocation}{" "}
              <button
                onClick={toggleMap}
                className="ml-2 text-sm text-gray-600 underline focus:outline-none hover:text-green-500"
              >
                <BsArrowRightCircle />
              </button>
            </p>
            <p className="text-sm text-gray-600">
              Max Players: {invitation.MaxPlayerCount}
            </p>
            <p className="text-sm text-gray-600">
              Accepted: {acceptedParticipants}/{invitation.MaxPlayerCount}{" "}
              {createdInvitation && (
                <div className="flex justify-center">
                  <button
                    onClick={() => setParcipantsOpen(true)}
                    className="h-[40px] w-[140px] m-3 bg-green-500 rounded-md text-black font-bold hover:bg-green-600"
                  >
                    View Parcipants
                  </button>
                </div>
              )}
            </p>
          </div>
          {itsInvitation && (
            <div>
              <button
                onClick={() => onAccept(invitation.InvitationId)}
                className="px-4 py-2 text-base font-medium text-white bg-green-500 rounded-lg hover:bg-green-600 focus:outline-none focus:ring-2 focus:ring-green-400 focus:ring-opacity-75 mr-4"
              >
                Accept Invitation
              </button>
              <button
                onClick={() => onReject(invitation.InvitationId)}
                className="px-4 py-2 text-base font-medium text-white bg-red-500 rounded-lg hover:bg-red-600 focus:outline-none focus:ring-2 focus:ring-red-400 focus:ring-opacity-75"
              >
                Reject Invitation
              </button>
            </div>
          )}
        </div>
      </div>
      {showMap && (
        <div className="fixed z-10 inset-0 overflow-y-auto">
          <div className="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
            <div className="fixed inset-0 transition-opacity">
              <div className="absolute inset-0 bg-gray-500 opacity-75"></div>
            </div>
            <span className="hidden sm:inline-block sm:align-middle sm:h-screen"></span>
            &#8203;
            <div
              className="inline-block align-bottom bg-white rounded-lg px-4 pt-5 pb-4 text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full sm:p-6"
              role="dialog"
              aria-modal="true"
              aria-labelledby="modal-headline"
            >
              <div className="mt-3 text-center sm:mt-0 sm:text-left">
                <h3
                  className="text-lg leading-6 font-medium text-gray-900"
                  id="modal-headline"
                >
                  {invitation.EventFullLocation}
                </h3>
                <div className="mt-2"></div>
              </div>
              <div style={{ height: "500px" }} className=" rounded-lg">
                <MapContainer
                  center={[invitation.Map_X_Cords, invitation.Map_Y_Cords]}
                  zoom={13}
                  scrollWheelZoom={false}
                  style={{ height: "100%", minHeight: "100%" }}
                >
                  <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                  />
                  <Marker
                    draggable={false}
                    icon={icon}
                    position={[invitation.Map_X_Cords, invitation.Map_Y_Cords]}
                  ></Marker>
                </MapContainer>
              </div>
              <div className="mt-5 sm:mt-4 sm:flex sm:flex-row-reverse">
                <button
                  type="button"
                  onClick={toggleMap}
                  className="w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 sm:ml-3 sm:w-auto sm:text-sm"
                >
                  Close
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
      {parcipantsOpen && (
        <ParcipantsModal
          setParticipantsCount={setAcceptedParticipants}
          participantsNumber={acceptedParticipants}
          activeGameId={invitation.ActiveGameId}
          onClose={() => setParcipantsOpen(false)}
        />
      )}
    </div>
  );
};

export default EventCard;
