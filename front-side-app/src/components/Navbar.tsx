import React, { useEffect, useState } from "react";
import { AiOutlineClose, AiOutlineMenu } from "react-icons/ai";
import { Link } from "react-router-dom";
import { useRecoilState } from "recoil";
import { validTokenAtom } from "../services/constants/globalStates";
import {
  CheckJWTAndSession,
  CheckJWTIsAdmin,
} from "../services/midlewear/AuthVerify";

export default function Navbar() {
  const [nav, setNav] = useState(true);
  const [tokenValid, setTokenValidation] = useState<boolean | undefined>(false);
  const [isAdmin, setIfAdmin] = useState<boolean | undefined>(false);
  const [validToken, setValidToken] = useRecoilState(validTokenAtom);

  const handleNav = () => {
    setNav(!nav);
  };

  const signOff = () => {
    localStorage.clear();
    window.location.replace("/");
  };

  return (
    <div>
      <header className="sticky top-0 bg-[#000300] z-10">
        <div className="flex justify-between items-center h-24 max-w-[1240px] mx-auto px-4 text-white">
          <Link to={"/"}>
            <h1 className=" w-full text-2xl font-bold text-[#00df9a]">
              TABLETOPGAMES.
            </h1>
          </Link>
          <ul className="hidden md:flex">
            <Link to={"/"}>
              <li className="p-4">Home</li>
            </Link>
            <Link to="/companies">
              <li className="p-4">Tabletop games</li>
            </Link>
            <li className="p-4">About</li>
            <li className="p-4">
              {!validToken ? (
                <Link to={"/login"}>
                  <button className="bg-white w-[150px] rounded-md font-medium py-2 my-[-20px] text-black hover:bg-green-600 hover:transition-colors">
                    Sign In
                  </button>
                </Link>
              ) : (
                <button
                  onClick={signOff}
                  className="bg-[#00df9a] w-[150px] rounded-md font-medium py-2 my-[-20px] text-black"
                >
                  Sign Off
                </button>
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
            <ul className="uppercase p-4">
              <Link to={"/"}>
                <li className="p-4 border-b border-gray-600">Home</li>
              </Link>
              <Link to="/companies">
                <li className="p-4 border-b border-gray-600">Tabletop games</li>
              </Link>
              <li className="p-4 border-b border-gray-600">About</li>
              {!tokenValid ? (
                <Link to={"/login"}>
                  <li className="p-4 border-b border-gray-600">Sign In</li>
                </Link>
              ) : (
                <Link onClick={signOff} to={"/"}>
                  <li className="p-4 border-b border-gray-600">Sign Off</li>
                </Link>
              )}

              {tokenValid && isAdmin ? (
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
