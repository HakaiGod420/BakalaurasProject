import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { useRecoilState } from "recoil";
import {
  getAcceptedInvitations,
  getActiveIntitations,
  updateInvitationState,
} from "../services/api/InvitationService";
import { acceptedInvitations } from "../services/constants/recoil/invitations";
import {
  InvitationStateChange,
  UserInvitation,
} from "../services/types/Invitation";
import EventAcceptedModal from "./EventAcceptedModal";
import EventCard from "./core/EventCard";
import LoadingComponent from "./core/LoadingComponent";
import SectionDivider from "./core/SectionDivider";

function EventsInvitationComponent() {
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 3;

  const handlePageChange = (pageNumber: number) => {
    setCurrentPage(pageNumber);
  };

  const [invitations, setInvitations] = useState<UserInvitation[]>([]);
  const [loading, setLoading] = useState(true);
  const [accpetedInvitation, setAcceptedInvitations] =
    useRecoilState(acceptedInvitations);

  const [showModal, setShowModal] = useState(false);
  const [selectedGameInvitation, setSelectedGameInvitation] = useState<
    UserInvitation | undefined
  >();

  const onClose = () => {
    setShowModal(false);
  };

  const openModel = (invitation: UserInvitation) => {
    console.log(invitation);
    setShowModal(true);
    setSelectedGameInvitation(invitation);
  };

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;

  const currentInvitations = invitations.slice(startIndex, endIndex);

  const totalPages = Math.ceil(invitations.length / itemsPerPage);

  const onAccept = async (invitationId: number) => {
    const loading = toast.loading("Accepting invitation...");
    const newState: InvitationStateChange = {
      InvitationId: invitationId,
      State: "accept",
    };
    await updateInvitationState(newState);

    setInvitations((invitations) =>
      invitations.filter(
        (invitation) => invitation.InvitationId !== invitationId
      )
    );

    toast.success("Invitation accepted", {
      id: loading,
    });
  };

  const onReject = async (invitationId: number) => {
    const loading = toast.loading("Rejecting invitation...");
    const newState: InvitationStateChange = {
      InvitationId: invitationId,
      State: "decline",
    };
    await updateInvitationState(newState);

    setInvitations((invitations) =>
      invitations.filter(
        (invitation) => invitation.InvitationId !== invitationId
      )
    );

    const responseAccepted = await getAcceptedInvitations();
    setAcceptedInvitations(responseAccepted);
    toast.success("Invitation rejected", {
      id: loading,
    });
  };

  useEffect(() => {
    const fetchUserSettings = async () => {
      const response = await getActiveIntitations();
      setInvitations(response);
      setLoading(false);
    };
    fetchUserSettings();
  }, []);

  return (
    <div className="mx-auto bg-white shadow-lg rounded-lg overflow-hidden">
      <SectionDivider label="Active Invitations" />
      <div className="max-w-7xl mx-auto py-6 px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {currentInvitations.map((invitation) => (
            <EventCard
              key={invitation.ActiveGameId}
              invitation={invitation}
              onAccept={() => openModel(invitation)}
              onReject={onReject}
              itsInvitation={true}
            />
          ))}
        </div>
        <div className="mt-4 flex justify-center">
          <nav className="inline-flex rounded-md shadow-sm -space-x-px ">
            {Array.from({ length: totalPages }, (_, index) => (
              <button
                key={index + 1}
                onClick={() => handlePageChange(index + 1)}
                className={`${
                  currentPage === index + 1
                    ? "bg-green-500 text-white"
                    : "bg-white text-gray-700 hover:bg-gray-50 border-gray-500"
                }
               font-medium rounded-md  px-4 py-2 mr-5 hover:text-black focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500`}
              >
                {index + 1}
              </button>
            ))}
          </nav>
        </div>
        {invitations.length === 0 && (
          <div>
            {!loading ? (
              <p className="text-gray-700 text-[50px] font-bold flex justify-center opacity-25">
                No invitations
              </p>
            ) : (
              <LoadingComponent />
            )}
          </div>
        )}
      </div>
      {showModal && (
        <EventAcceptedModal
          onClose={onClose}
          userInvitation={selectedGameInvitation}
          onAccept={onAccept}
        />
      )}
    </div>
  );
}
export default EventsInvitationComponent;
