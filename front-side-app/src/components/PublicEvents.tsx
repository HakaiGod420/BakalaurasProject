import { useState } from "react";
import SectionDivider from "./core/SectionDivider";

interface Event {
  id: number;
  title: string;
  date: string;
  location: string;
  maxPlayers: number;
  acceptedPlayers: number;
  imageUrl: string;
}

const TEMP_DATA: Event[] = [
  {
    id: 1,
    title: "Event 1",
    date: "2023-04-30",
    location: "Location 1",
    maxPlayers: 5,
    acceptedPlayers: 2,
    imageUrl: "https://picsum.photos/id/237/200/200",
  },
  {
    id: 2,
    title: "Event 2",
    date: "2023-05-01",
    location: "Location 2",
    maxPlayers: 10,
    acceptedPlayers: 5,
    imageUrl: "https://picsum.photos/id/238/200/200",
  },
  {
    id: 3,
    title: "Event 3",
    date: "2023-05-02",
    location: "Location 3",
    maxPlayers: 8,
    acceptedPlayers: 3,
    imageUrl: "https://picsum.photos/id/239/200/200",
  },
  {
    id: 4,
    title: "Event 4",
    date: "2023-05-03",
    location: "Location 4",
    maxPlayers: 4,
    acceptedPlayers: 1,
    imageUrl: "https://picsum.photos/id/240/200/200",
  },
  {
    id: 5,
    title: "Event 5",
    date: "2023-05-04",
    location: "Location 5",
    maxPlayers: 6,
    acceptedPlayers: 2,
    imageUrl: "https://picsum.photos/id/241/200/200",
  },
  {
    id: 6,
    title: "Event 6",
    date: "2023-05-05",
    location: "Location 6",
    maxPlayers: 3,
    acceptedPlayers: 1,
    imageUrl: "https://picsum.photos/id/242/200/200",
  },
];

function PublicEvents() {
  const [currentPage, setCurrentPage] = useState(1);
  const [eventsPerPage] = useState(3);

  const indexOfLastEvent = currentPage * eventsPerPage;
  const indexOfFirstEvent = indexOfLastEvent - eventsPerPage;
  const currentEvents = TEMP_DATA.slice(indexOfFirstEvent, indexOfLastEvent);
  const totalPages = Math.ceil(TEMP_DATA.length / eventsPerPage);

  const paginate = (pageNumber: number) => setCurrentPage(pageNumber);

  return (
    <div className="bg-white">
      <SectionDivider label="All events in Lithuania" />
      <div className="flex flex-wrap mx-auto  max-w-[1280px] justify-center">
        {currentEvents.map((event) => (
          <div key={event.id} className="w-full md:w-1/3 lg:w-1/4 p-4  ">
            <div className="bg-white rounded-lg shadow-lg flex flex-col h-full">
              <img
                src={event.imageUrl}
                alt={event.title}
                className="w-full rounded-t-lg h-48 object-cover"
              />
              <div className="flex-1 p-4">
                <h2 className="text-xl font-bold mb-2">{event.title}</h2>
                <p className="text-gray-700 mb-1">
                  <span className="font-bold">Date:</span> {event.date}
                </p>
                <p className="text-gray-700 mb-1">
                  <span className="font-bold">Location:</span> {event.location}
                </p>
                <p className="text-gray-700 mb-1">
                  <span className="font-bold">Max Players:</span>{" "}
                  {event.maxPlayers}
                </p>
                <p className="text-gray-700 mb-1">
                  <span className="font-bold">Accepted Players:</span>{" "}
                  {event.acceptedPlayers}
                </p>
                <button className="text-white bg-green-500 py-2 px-4 rounded mt-2 ml-auto">
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
      </div>
    </div>
  );
}

export default PublicEvents;
