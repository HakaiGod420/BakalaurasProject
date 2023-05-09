import dayjs from "dayjs";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import {
  getInvitationList,
  joinToInvitation,
} from "../services/api/InvitationService";
import { SERVER_API } from "../services/constants/ClienConstants";
import { InvitationItem, UserInvitation } from "../services/types/Invitation";
import EventAcceptedModal from "./EventAcceptedModal";
import SectionDivider from "./core/SectionDivider";

function PublicEvents() {
  const [currentPage, setCurrentPage] = useState(1);
  const [eventsPerPage] = useState(3);
  const [totalPages, setTotalPages] = useState(1);
  const navigate = useNavigate();
  const [activeInvitations, setActiveInvitations] = useState<
    InvitationItem[] | undefined
  >([]);

  const [isOpen, setIsOpen] = useState(false);
  const [invitation, setInvitation] = useState<UserInvitation>();

  const onAccept = async (invitationId: number) => {
    const loading = toast.loading("Accepting invitation...");

    await joinToInvitation(invitationId);

    toast.success("Invitation accepted", {
      id: loading,
    });
    navigate("/myeventes/accepted");
  };

  const paginate = async (pageNumber: number) => {
    const response = await getInvitationList(
      "Lithuania",
      pageNumber - 1,
      eventsPerPage
    );
    setActiveInvitations(response?.Invitations);
    setCurrentPage(pageNumber);
  };

  const openModel = (invitation: InvitationItem) => {
    const newInvitationAcception: UserInvitation = {
      InvitationId: invitation.InvitationId,
      ActiveGameId: 0,
      BoardGameTitle: invitation.BoardGameTitle,
      BoardGameId: invitation.BoardGameId,
      EventDate: invitation.Date,
      EventFullLocation: invitation.Location,
      MaxPlayerCount: invitation.MaxPlayer,
      AcceptedCount: invitation.AcceptedPlayer,
      Map_X_Cords: 0,
      Map_Y_Cords: 0,
    };
    setInvitation(newInvitationAcception);
    setIsOpen(true);
  };

  const onClose = () => {
    setIsOpen(false);
  };
  useEffect(() => {
    const getInvitations = async () => {
      const response = await getInvitationList(
        "Lithuania",
        currentPage - 1,
        eventsPerPage
      );
      setActiveInvitations(response?.Invitations);
      const totalPages = Math.ceil(
        response ? response.TotalCount / eventsPerPage : 1
      );
      setTotalPages(totalPages);
    };

    getInvitations();
  }, []);

  return (
    <div className="bg-white">
      <SectionDivider label="All events in Lithuania" />
      <div className="flex flex-wrap mx-auto  max-w-[1280px] justify-center">
        {activeInvitations?.map((event) => (
          <div
            key={event.InvitationId}
            className="w-full md:w-1/3 lg:w-1/4 p-4  "
          >
            <div className="bg-white rounded-lg shadow-lg flex flex-col h-full">
              <img
                src={SERVER_API + "/" + event.ImageUrl}
                alt={event.BoardGameTitle}
                className="w-full rounded-t-lg h-48 object-cover"
              />
              <div className="flex-1 p-4">
                <h2 className="text-xl font-bold mb-2">
                  {event.BoardGameTitle}
                </h2>
                <p className="text-gray-700 mb-1">
                  <span className="font-bold">Date:</span>{" "}
                  {dayjs(event.Date).format("YYYY-MM-DD HH:mm")}
                </p>
                <p className="text-gray-700 mb-1">
                  <span className="font-bold">Location:</span> {event.Location}
                </p>
                <p className="text-gray-700 mb-1">
                  <span className="font-bold">Max Players:</span>{" "}
                  {event.MaxPlayer}
                </p>
                <p className="text-gray-700 mb-1">
                  <span className="font-bold">Accepted Players:</span>{" "}
                  {event.AcceptedPlayer}
                </p>
                <button
                  onClick={() => openModel(event)}
                  className="text-white bg-green-500 py-2 px-4 rounded mt-2 ml-auto"
                >
                  Join
                </button>
              </div>
            </div>
          </div>
        ))}

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
        {activeInvitations?.length === 0 && (
          <div className="text-[50px] text-black p-[200px]">
            <p>No events found</p>
          </div>
        )}
      </div>
      {isOpen && (
        <EventAcceptedModal
          userInvitation={invitation}
          onClose={onClose}
          onAccept={onAccept}
        />
      )}
    </div>
  );
}

export default PublicEvents;
