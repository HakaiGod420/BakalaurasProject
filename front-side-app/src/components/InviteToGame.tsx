import dayjs from "dayjs";
import React, { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { useParams } from "react-router-dom";
import {
  getCreatedInvitations,
  sentPersonalInvitation,
} from "../services/api/InvitationService";
import {
  SentPersonalInvitation,
  UserInvitation,
} from "../services/types/Invitation";

interface Props {
  modalOpen: boolean;
  setModalOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const InviteToGameFromProfile: React.FC<Props> = ({
  modalOpen,
  setModalOpen,
}: Props) => {
  const [invitations, setInvitations] = useState<UserInvitation[]>([]);
  const [selectedInvitation, setSelectedInvitation] =
    useState<UserInvitation | null>(null);

  const params = useParams();


  const handleCloseModal = () => {
    setModalOpen(false);
  };

  const handleSelectInvitation = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    const selectedId = Number(event.target.value);
    const selected = invitations.find(
      (invitation) => invitation.ActiveGameId === selectedId
    );
    setSelectedInvitation(selected || null);
  };

  const handleSubmit = async () => {
    const loading = toast.loading("Sending invitation...");
    if (selectedInvitation) {
      const newInvitation: SentPersonalInvitation = {
        UserName: params.username,
        ActiveInvitationId: selectedInvitation.ActiveGameId,
      };
      await sentPersonalInvitation(newInvitation);
      toast.success("Invitation successfully sended", {
        id: loading,
      });
    }

    handleCloseModal();
  };

  useEffect(() => {
    const fetchCreatedInvitations = async () => {
      const response = await getCreatedInvitations();
      setInvitations(response);
      //setLoading(false);
    };
    console.log(invitations);
    fetchCreatedInvitations();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      <div
        className={`fixed z-10 inset-0 overflow-y-auto ${
          modalOpen ? "block" : "hidden"
        }`}
      >
        <div className="flex items-center justify-center min-h-screen px-4">
          <div className="fixed inset-0 transition-opacity">
            <div className="absolute inset-0 bg-gray-500 opacity-75"></div>
          </div>

          <div className="bg-white rounded-lg overflow-hidden shadow-xl transform transition-all max-w-md w-full">
            <div className="bg-white px-4 py-5 sm:p-6">
              <div className="sm:flex sm:items-start">
                <div className="w-full mt-3 text-center sm:mt-0 sm:ml-4 sm:text-left">
                  <h1 className="text-xl font-bold mb-4">Select Invitation</h1>

                  <select
                    className="mb-4 w-full border-2 border-gray-300 rounded-md p-2 bg-white"
                    value={selectedInvitation?.ActiveGameId || ""}
                    onChange={handleSelectInvitation}
                  >
                    <option value="">Select an Invitation</option>
                    {invitations.map((invitation) => (
                      <option
                        key={invitation.ActiveGameId}
                        value={invitation.ActiveGameId}
                      >
                        {`${invitation.BoardGameTitle} ${dayjs(
                          invitation.EventDate
                        ).format("YYYY-MM-DD HH:mm")}`}
                      </option>
                    ))}
                  </select>
                </div>
              </div>
            </div>

            <div className="bg-gray-50 px-4 py-3 sm:px-6 sm:flex sm:flex-row-reverse">
              <button
                className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded"
                onClick={handleCloseModal}
              >
                Cancel
              </button>
              <button
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mr-2"
                onClick={handleSubmit}
              >
                Submit
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default InviteToGameFromProfile;
