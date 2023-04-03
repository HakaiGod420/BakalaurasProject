import { FaCalendarAlt, FaUser } from "react-icons/fa";

const GameBoardCard: React.FC = () => {
  return (
    <div className="max-w-screen-xl mx-auto p-4 flex flex-col md:flex-row items-center md:items-start">
      <div className="h-48 md:h-auto w-full md:w-1/2 lg:w-1/3 bg-gray-100 relative mb-4 md:mb-0 md:mr-4">
        <img
          src="https://image.pbs.org/bento3-prod/pbsutah/blogs/kids/2021/bfaf8b588a_Missy%27s%20DIY%20Board%20Game%20example.jpg"
          alt={`Cover image for`}
          className="object-cover w-full h-full"
        />
      </div>
      <div className="w-full h-[306px]  md:w-1/2 lg:w-2/3 bg-white rounded-md shadow-xl overflow-hidden p-4 md:p-6 flex flex-col justify-between">
        <div>
          <h2 className="text-lg font-medium text-gray-800">
            Some awesome name that I've never heard before
          </h2>
          <div className="flex flex-wrap items-center mt-2">
            <p className="text-sm font-medium text-gray-700 mr-2 mb-2 md:mb-0">
              Categories:
            </p>
            <span className="text-sm font-medium text-gray-500">
              Strategy, Action, Family, and more...
            </span>
            <span className="mx-2 text-gray-400">&bull;</span>
            <p className="text-sm font-medium text-gray-700 mr-2 mb-2 md:mb-0">
              Types:
            </p>
            <span className="text-sm font-medium text-gray-500">
              Strategy, Action, Family, and more...
            </span>
          </div>
          <div className="mt-2 flex flex-wrap items-center">
            <FaCalendarAlt className="h-5 w-5 text-gray-400" />
            <p className="ml-2 text-sm font-medium text-gray-500">
              Created: 2000-01-01
            </p>
            <span className="mx-2 text-gray-400">&bull;</span>
            <FaCalendarAlt className="h-5 w-5 text-gray-400" />
            <p className="ml-2 text-sm font-medium text-gray-500">
              Last updated: 2000-01-01
            </p>
          </div>
          <div className="mt-2 flex flex-wrap items-center">
            <FaUser className="h-5 w-5 text-gray-400" />
            <p className="ml-2 text-sm font-medium text-gray-500">
              Creator: gytis
            </p>
          </div>
        </div>
        <div className="flex justify-between mt-4">
          <button className="bg-blue-500 hover:bg-blue-600 text-white font-medium py-2 px-4 rounded mr-2">
            Create Invitation
          </button>
          <button className="bg-gray-300 hover:bg-gray-400 text-gray-700 font-medium py-2 px-4 rounded">
            Rate
          </button>
        </div>
      </div>
    </div>
  );
};

export default GameBoardCard;
