import { useState } from "react";
import { AiOutlineClose, AiOutlineMenu } from "react-icons/ai";
import { Link } from "react-router-dom";
import { useRecoilState } from "recoil";
import { validTokenAtom } from "../services/constants/globalStates";

export default function Navbar() {
  const [nav, setNav] = useState(true);
  const [isAdmin] = useState<boolean | undefined>(false);
  const [validToken] = useRecoilState(validTokenAtom);

  const handleNav = () => {
    setNav(!nav);
  };

  const signOff = () => {
    localStorage.clear();
    window.location.replace("/");
  };

  return (
    <div>
      <script src="https://unpkg.com/@themesberg/flowbite@1.1.1/dist/flowbite.bundle.js"></script>
      <header className="sticky top-0 bg-[#000300] z-10">
        <div className="flex justify-between items-center h-24 max-w-[1240px] mx-auto px-4 text-white">
          <Link to={"/"}>
            <h1 className=" w-full text-2xl font-bold text-[#00df9a]">
              TABLETOPGAMES.
            </h1>
          </Link>
          <ul className="hidden md:flex">
            <li className="p-4 m-auto">
              <ul className="flex-col md:flex-row flex md:space-x-8 mt-4 md:mt-0 md:text-sm md:font-medium">
                <li>
                  <Link
                    to="#"
                    className=" md:bg-transparent text-white block pl-3 pr-4 py-2 md:text-white md:hover:text-green-700 md:p-0 rounded focus:outline-none"
                    aria-current="page"
                  >
                    Home
                  </Link>
                </li>
                <li>
                  <Link
                    to="#"
                    className="text-white hover:bg-gray-50 border-b border-gray-100 md:hover:bg-transparent md:border-0 block pl-3 pr-4 py-2 md:hover:text-green-700 md:p-0"
                  >
                    Table Game Boards
                  </Link>
                </li>
                <li>
                  <Link
                    to="#"
                    className="text-white hover:bg-gray-50 border-b border-gray-100 md:hover:bg-transparent md:border-0 block pl-3 pr-4 py-2 md:hover:text-green-700 md:p-0"
                  >
                    Pricing
                  </Link>
                </li>
                <li>
                  <Link
                    to="#"
                    className="text-white hover:bg-gray-50 border-b border-gray-100 md:hover:bg-transparent md:border-0 block pl-3 pr-4 py-2 md:hover:text-green-700 md:p-0"
                  >
                    Contact
                  </Link>
                </li>
              </ul>
            </li>
            <li className="p-4">
              {!validToken ? (
                <Link to={"/login"}>
                  <button className="bg-white w-[150px] rounded-md font-medium py-2 my-[-20px] text-black hover:bg-green-600 hover:transition-colors">
                    Sign In
                  </button>
                </Link>
              ) : (
                <div className="dropdown dropdown-end">
                  <label
                    tabIndex={0}
                    className="btn btn-ghost btn-circle avatar"
                  >
                    <div className="w-10 rounded-full">
                      <img
                        src={require("../assets/images/profileImageDefault.png")}
                        alt="profileImg"
                      />
                    </div>
                  </label>
                  <ul
                    tabIndex={0}
                    className="menu menu-compact dropdown-content mt-3 p-2 shadow bg-base-100 rounded-box w-52"
                  >
                    <li>
                      <Link to={"#"} className="justify-between">
                        Profile
                        <span className="badge">New</span>
                      </Link>
                    </li>
                    <li>
                      <Link to={"#"}>Settings</Link>
                    </li>
                    <li>
                      <Link onClick={signOff} to={""}>
                        Logout
                      </Link>
                    </li>
                  </ul>
                </div>
              )}
            </li>
            <li className="p-4">
              {validToken && isAdmin ? (
                <Link to={"/admin"}>
                  <button className="bg-white w-[150px] rounded-md font-medium py-2 my-[-20px] text-black">
                    Admin CP
                  </button>
                </Link>
              ) : null}
            </li>
          </ul>
          <div className="block md:hidden" onClick={handleNav}>
            {!nav ? <AiOutlineClose size={20} /> : <AiOutlineMenu size={20} />}
          </div>

          <div
            className={
              /*later fix animation */ !nav
                ? "fixed left-0 top-0 w-[60%] h-full border-r border-r-gray-900 bg-[#000300] ease-in-out duration-500"
                : "fixed hidden left-[-100%]"
            }
          >
            <h1 className=" w-full text-2xl font-bold text-[#00df9a] m-4">
              TABLETOPGAMES.
            </h1>
            {validToken && (
              <ul className="uppercase p-4">
                <p className="uppercase text-green-600">User Menu</p>
                <li className="p-4 border-b border-gray-600">
                  <Link to="#">My Profile</Link>
                </li>
                <li className="p-4 border-b border-gray-600">
                  <Link to="#">Settings</Link>
                </li>
              </ul>
            )}

            <ul className="uppercase p-4">
              <p className="uppercase text-green-600">Menu</p>
              <Link to={"/"}>
                <li className="p-4 border-b border-gray-600">Home</li>
              </Link>
              <Link to="/companies">
                <li className="p-4 border-b border-gray-600">Tabletop games</li>
              </Link>
              <li className="p-4 border-b border-gray-600">About</li>
              {!validToken ? (
                <Link onClick={() => setNav(true)} to={"/login"}>
                  <li className="p-4 border-b border-gray-600">Sign In</li>
                </Link>
              ) : (
                <Link onClick={signOff} to={"/"}>
                  <li className="p-4 border-b border-gray-600">Sign Off</li>
                </Link>
              )}

              {validToken && isAdmin ? (
                <Link to={"/admin"}>
                  <li className="p-4 border-b border-gray-600">Admin CP</li>
                </Link>
              ) : null}
            </ul>
          </div>
        </div>
      </header>
    </div>
  );
}
