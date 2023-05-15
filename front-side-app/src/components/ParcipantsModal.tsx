import { useEffect, useState } from "react";
import { toast } from "react-hot-toast";
import { Link } from "react-router-dom";
import {
  changeParticipantState,
  getParticipants,
} from "../services/api/InvitationService";
import { Participant, ParticipationState } from "../services/types/Invitation";

type ModalProps = {
  onClose: () => void;
  activeGameId: number;
};

const ParcipantsModal: React.FC<ModalProps> = ({ onClose, activeGameId }) => {
  const [participants, setParticipants] = useState<Participant[] | undefined>(
    []
  );

  const updateParticipant = async (participantId: number) => {
    const loading = toast.loading("Updating participant state...");
    const newState: ParticipationState = {
      ActiveGameId: activeGameId,
      UserId: participantId,
      IsBlocked: !participants?.find(
        (participant) => participant.UserId === participantId
      )?.IsBlocked as boolean,
    };

    const response = await changeParticipantState(newState);
    console.log(response);
    if (response) {
      toast.success("Successfully changed participant state", {
        id: loading,
      });
    } else {
      toast.error("Failed to change participant state game", {
        id: loading,
      });
    }

    setParticipants((prevParticipants) => {
      if (prevParticipants) {
        const updatedParticipants = prevParticipants.map((participant) => {
          if (participant.UserId === participantId) {
            return {
              ...participant,
              IsBlocked: !participant.IsBlocked, // Toggle the value of isBlocked
            };
          }
          return participant;
        });
        return updatedParticipants;
      }
      return prevParticipants;
    });
  };

  useEffect(() => {
    const getParticipantsOfGame = async () => {
      const response = await getParticipants(activeGameId);
      console.log(response);
      setParticipants(response);
    };
    getParticipantsOfGame();
  }, [activeGameId]);

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
      <div className="bg-gray-800 rounded-lg p-6 max-w-[500px] w-full">
        <h2 className="text-xl font-semibold mb-4 text-white">Participants</h2>
        {participants?.length === 0 ? (
          <p className="text-center text-white">No participants</p>
        ) : (
          <table className="w-full bg-gray-700 text-white rounded-md">
            <thead>
              <tr>
                <th className="px-4 py-2 rounded-tl-lg">Username</th>
                <th className="rounded-tr-lg">Block</th>
                <th>Profile</th>
              </tr>
            </thead>
            <tbody>
              {participants?.map((participant) => (
                <tr
                  key={participant.UserId}
                  className="hover:bg-gray-600 bg-gray-500"
                >
                  <td className="px-4 py-2">
                    <span>{participant.UserName}</span>
                  </td>
                  <td className="flex justify-center">
                    {participant.IsBlocked ? (
                      <button
                        onClick={() => updateParticipant(participant.UserId)}
                        className="text-black bg-green-500 p-2 m-1 rounded-md font-bold w-20 hover-bg-green-600s"
                      >
                        Unblock
                      </button>
                    ) : (
                      <button
                        onClick={() => updateParticipant(participant.UserId)}
                        className="text-black bg-red-500 p-2 m-1 rounded-md font-bold w-20 hover:bg-red-600"
                      >
                        Block
                      </button>
                    )}
                  </td>
                  <td>
                    <Link to={"/profile/" + participant.UserName}>
                      <button className="text-black bg-gray-200 p-2 m-1 rounded-md font-bold w-30 hover:bg-gray-300">
                        Go To Profile
                      </button>
                    </Link>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
        <button
          onClick={onClose}
          className="mt-4 px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600"
        >
          Close
        </button>
      </div>
    </div>
  );
};

export default ParcipantsModal;
