import { useEffect, useState } from "react";
import {
  getCreatedInvitations,
  updateInvitationState,
} from "../services/api/InvitationService";
import {
  InvitationStateChange,
  UserInvitation,
} from "../services/types/Invitation";
import EventCard from "./core/EventCard";
import LoadingComponent from "./core/LoadingComponent";
import SectionDivider from "./core/SectionDivider";

function CreatedInvitationList() {
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 3;

  const handlePageChange = (pageNumber: number) => {
    setCurrentPage(pageNumber);
  };

  const [invitations, setInvitations] = useState<UserInvitation[]>([]);

  const [loading, setLoading] = useState(true);

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;

  const currentInvitations = invitations.slice(startIndex, endIndex);

  const totalPages = Math.ceil(invitations.length / itemsPerPage);

  const onAccept = async (invitationId: number) => {
    console.log("sad");
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
  };

  const onReject = async (invitationId: number) => {
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
  };

  useEffect(() => {
    const fetchCreatedInvitations = async () => {
      const response = await getCreatedInvitations();
      setInvitations(response);
      setLoading(false);
    };
    console.log(invitations);
    fetchCreatedInvitations();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div className="mx-auto bg-white  shadow-lg rounded-lg overflow-hidden">
      <SectionDivider label="Created Invitations" />
      <div className="max-w-7xl mx-auto py-6 px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {currentInvitations.map((invitation) => (
            <EventCard
              key={invitation.ActiveGameId}
              invitation={invitation}
              onAccept={onAccept}
              onReject={onReject}
              itsInvitation={false}
              createdInvitation={true}
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
              <p className="text-gray-700 text-[50px] font-bold flex justify-center opacity-25 p-20">
                No created invitations
              </p>
            ) : (
              <LoadingComponent />
            )}
          </div>
        )}
      </div>
    </div>
  );
}
export default CreatedInvitationList;
