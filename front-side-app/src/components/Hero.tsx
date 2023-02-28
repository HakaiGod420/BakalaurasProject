import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { TypeAnimation } from "react-type-animation";
import { useRecoilState } from "recoil";
import { validTokenAtom } from "../services/constants/globalStates";

export default function Hero() {
  const [validToken, setValidToken] = useRecoilState(validTokenAtom);

  return (
    <div className="text-white">
      <div className="max-w-[800px] mt-[-96px] w-full h-screen mx-auto text-center flex flex-col justify-center">
        <p className="text-[#00df9a] font-bold p-2">
          EVERY DAY WE UPDATE WITH NEW TABLETOP BOARD GAMES
        </p>
        <h1 className="md:text-7xl sm:text-6xl text-4xl font-bold md:py-6">
          RATE, REVIEW AND INVITE TO PLAY YOUR FAVORITE TABLETOP GAME
        </h1>
        <div className="flex justify-center items-center">
          <p className="md:text-4xl sm:text-3xl text-xl font-bold py-4">
            Rate, view and play
          </p>
          <TypeAnimation
            className="md:text-4xl sm:text-3xl text-xl font-bold pl-2 text-gray-500 md:pl-4"
            sequence={[
              "Gloomhaven",
              2000,
              "Wingspan",
              2000,
              "Azul: Summer Pavilion",
              2000,
              () => {
                // Place optional callbacks anywhere in the array
              },
            ]}
            repeat={Infinity}
            speed={10}
            deletionSpeed={10}
          />
        </div>
        {!validToken ? (
          <Link to={"/login"}>
            <button className="bg-[#00df9a] w-[200px] rounded-md font-medium my-6 mx-auto py-3 text-black hover:bg-white hover:transition-colors">
              Sign In
            </button>
          </Link>
        ) : null}
      </div>
    </div>
  );
}
