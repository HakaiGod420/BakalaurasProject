import { IconType } from "react-icons";
import { AiOutlineDashboard } from "react-icons/ai";
import { BiGame } from "react-icons/bi";
import { Link } from "react-router-dom";

interface MenuItem {
  label: string;
  icon: IconType;
  to: string;
  description: string;
}

const menuItems: MenuItem[] = [
  {
    label: "View Tabletop Game Boards",
    icon: BiGame,
    to: "gameboards",
    description: "View all tabletop game boards that have been created.",
  },
  {
    label: "Review Created Tabletop Games",
    icon: BiGame,
    to: "review-gameboard",
    description:
      "Review and approve or reject new tabletop games created by users.",
  },
];

function AdminMenuListComponent() {
  return (
    <div className="flex flex-col h-[70vh] lg:h-[50vh] bg-white">
      <div className="flex justify-center items-center py-4 sm:py-8">
        <div className="max-w-screen-lg w-full">
          <div className="flex justify-between items-center">
            <Link to="/admin" className="text-xl font-bold">
              <AiOutlineDashboard className="inline-block mr-2 mb-1 text-green-500" />
              Admin Dashboard
            </Link>
            <button
              className="sm:hidden p-2 rounded-md text-gray-900 hover:bg-gray-200 focus:outline-none focus:bg-gray-200"
              onClick={() => console.log("Menu button clicked")}
            >
              <svg
                viewBox="0 0 20 20"
                fill="currentColor"
                className="menu w-6 h-6"
              >
                <path
                  fillRule="evenodd"
                  d="M3 4a1 1 0 011-1h12a1 1 0 010 2H4a1 1 0 01-1-1zM4 9a1 1 0 100 2h12a1 1 0 100-2H4zm-1 5a1 1 0 011-1h12a1 1 0 010 2H4a1 1 0 01-1-1z"
                  clipRule="evenodd"
                ></path>
              </svg>
            </button>
          </div>
        </div>
      </div>
      <div className="flex-grow flex justify-center items-center px-20">
        <div className="max-w-screen-lg w-full grid gap-8 sm:grid-cols-1 md:grid-cols-1 lg:grid-cols-2 xl:grid-cols-2  ">
          {menuItems.map((item, index) => (
            <Link
              key={item.to}
              to={item.to}
              className={`bg-gray-900 text-gray-100 rounded-md overflow-hidden shadow-md hover:shadow-lg hover:border-green-600 hover:border-2 lg:h-[125px]`}
            >
              <div className="p-6 h-full flex flex-col justify-between">
                <div className="flex items-center">
                  <item.icon className="text-3xl mr-2 text-green-500" />
                  <div>
                    <h2 className="text-lg font-bold">{item.label}</h2>
                    <p className="text-sm text-gray-400">{item.description}</p>
                  </div>
                </div>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </div>
  );
}

export default AdminMenuListComponent;
