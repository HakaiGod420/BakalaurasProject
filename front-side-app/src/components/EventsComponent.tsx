import { useState } from "react";
import { UserInvitation } from "../services/types/Invitation";
import EventCard from "./core/EventCard";
import SectionDivider from "./core/SectionDivider";

const invitations: UserInvitation[] = [
  {
    ActiveGameId: 1,
    BoardGameTitle: "Birthday Party",
    EventDate: "May 5th, 2023 at 7:00 PM",
    EventFullLocation: "123 Main St, Anytown, USA",
    MaxPlayerCount: 10,
    AcceptedCount: 7,
    Map_X_Cords: 55.55,
    Map_Y_Cords: 55.55,
    BoardGameId: 1,
  },
  {
    ActiveGameId: 2,
    BoardGameTitle: "Birthday Party",
    EventDate: "May 5th, 2023 at 7:00 PM",
    EventFullLocation: "123 Main St, Anytown, USA",
    MaxPlayerCount: 10,
    AcceptedCount: 7,
    Map_X_Cords: 55.55,
    Map_Y_Cords: 55.55,
    BoardGameId: 1,
  },
  {
    ActiveGameId: 3,
    BoardGameTitle: "Birthday Party",
    EventDate: "May 5th, 2023 at 7:00 PM",
    EventFullLocation: "123 Main St, Anytown, USA",
    MaxPlayerCount: 10,
    AcceptedCount: 7,
    Map_X_Cords: 55.55,
    Map_Y_Cords: 55.55,
    BoardGameId: 1,
  },
  {
    ActiveGameId: 4,
    BoardGameTitle: "Birthday Party",
    EventDate: "May 5th, 2023 at 7:00 PM",
    EventFullLocation: "123 Main St, Anytown, USA",
    MaxPlayerCount: 10,
    AcceptedCount: 7,
    Map_X_Cords: 55.55,
    Map_Y_Cords: 55.55,
    BoardGameId: 1,
  },
  {
    ActiveGameId: 5,
    BoardGameTitle: "Birthday Party",
    EventDate: "May 5th, 2023 at 7:00 PM",
    EventFullLocation: "123 Main St, Anytown, USA",
    MaxPlayerCount: 10,
    AcceptedCount: 7,
    Map_X_Cords: 55.55,
    Map_Y_Cords: 55.55,
    BoardGameId: 1,
  },
  {
    ActiveGameId: 6,
    BoardGameTitle: "Birthday Party",
    EventDate: "May 5th, 2023 at 7:00 PM",
    EventFullLocation: "123 Main St, Anytown, USA",
    MaxPlayerCount: 10,
    AcceptedCount: 7,
    Map_X_Cords: 55.55,
    Map_Y_Cords: 55.55,
    BoardGameId: 1,
  },
];

function EventsComponent() {
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 3;

  const handlePageChange = (pageNumber: number) => {
    setCurrentPage(pageNumber);
  };

  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;

  const currentInvitations = invitations.slice(startIndex, endIndex);

  const totalPages = Math.ceil(invitations.length / itemsPerPage);

  return (
    <div className="mx-auto bg-white border-2 border-green-500 shadow-lg rounded-lg overflow-hidden">
      <SectionDivider label="Active Invitations" />
      <div className="max-w-7xl mx-auto py-6 px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {currentInvitations.map((invitation) => (
            <EventCard
              key={invitation.ActiveGameId}
              invitation={invitation}
              onAccept={() => {}}
              onReject={() => {}}
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
            <p className="text-gray-700 text-[50px] font-bold flex justify-center">
              No invitations
            </p>
          </div>
        )}
      </div>
    </div>
  );
}

export default EventsComponent;
