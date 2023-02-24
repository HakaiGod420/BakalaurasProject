import React, { useState } from "react";
import { Link } from "react-router-dom";

function LoginComponent() {
  const [password, setPassword] = useState("");
  const [hidenErr, setErrShow] = useState(false);
  const [errorName, setErrorName] = useState("");
  const [username, setUsername] = useState("");

  return (
    <section className="text-gray-600 body-font bg-[#000300]">
      <div className="container px-5 py-24 mx-auto flex flex-wrap items-center max-w-[1240px]">
        <div className="lg:w-3/5 md:w-1/2 md:pr-16 lg:pr-0 pr-0">
          <h1 className="title-font font-medium text-3xl text-[#00df9a]">
            JOIN TO STRIVING COMMUNITY TODAY
          </h1>
        </div>
        <div className="lg:w-2/6 md:w-1/2 bg-gray-100 rounded-lg p-8 flex flex-col md:ml-auto w-full mt-10 md:mt-0 border-0 border-[#1c9e75] shadow-md  shadow-[#1c9e75]">
          <h2 className="text-gray-900 text-lg font-medium title-font mb-5">
            Sign In
          </h2>

          <div className="mb-4">
            <label
              htmlFor="username"
              className="leading-7 text-sm text-gray-600"
            >
              Username
            </label>
            <input
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              type="username"
              id="username"
              name="username"
              className="w-full bg-white rounded border border-gray-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 text-base outline-none text-gray-700 py-1 px-3 leading-8 transition-colors duration-200 ease-in-out"
            />
          </div>
          <div className="mb-4">
            <label
              htmlFor="password"
              className="leading-7 text-sm text-gray-600"
            >
              Password
            </label>
            <input
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              type="password"
              id="password"
              name="password"
              className="w-full bg-white rounded border border-gray-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 text-base outline-none text-gray-700 py-1 px-3 leading-8 transition-colors duration-200 ease-in-out"
            />
          </div>
          <button
            disabled={username === "" || password === ""}
            className="disabled:bg-gray-500 text-black bg-[#00df9a] border-0 py-2 px-8 focus:outline-none hover:bg-[#1c9e75] rounded text-lg font-bold"
          >
            Login
          </button>
          <div>
            {!hidenErr ? (
              <p className=" justify-center text-center text-xs font-bold text-[#ff4f4f] mt-3">
                {errorName}
              </p>
            ) : null}
          </div>
          <p className="text-xs text-gray-500 mt-3">
            Not a member?{" "}
            <Link
              className=" text-[#1c9e75] hover:text-[#0e3c2d] "
              to={"/register"}
            >
              Register
            </Link>
          </p>
        </div>
      </div>
    </section>
  );
}

export default LoginComponent;
