import { Alert } from "antd";
import axios from "axios";
import { useState } from "react";
import toast from "react-hot-toast";
import { Link, useNavigate } from "react-router-dom";
import { useRecoilState } from "recoil";
import { SERVER_API } from "../services/constants/ClienConstants";
import {
  isAdminAtom,
  validTokenAtom,
} from "../services/constants/recoil/globalStates";
import { CheckJWTIsAdmin } from "../services/midlewear/AuthVerify";
import { ErrorBasic } from "../services/types/Error";
import { LoginData } from "../services/types/User";

function LoginComponent() {
  const [password, setPassword] = useState("");
  const [errorName, setErrorName] = useState("");
  const [username, setUsername] = useState("");

  const [showError, setShowError] = useState(false);

  const navigate = useNavigate();

  const [, setValidToken] = useRecoilState(validTokenAtom);
  const [, setIsAdmin] = useRecoilState(isAdminAtom);

  const login = async () => {
    const crediantals: LoginData = {
      userName: username,
      password: password,
    };

    const loading = toast.loading("Trying to log in");
    await axios
      .post(SERVER_API + "/api/user/login", crediantals)
      .then((res) => {
        setShowError(false);
        toast.success("Successfully login in", {
          id: loading,
        });
        localStorage.setItem("token", JSON.stringify({ token: res.data }));
        setValidToken(true);
        navigate("/");
      })
      .catch((error) => {
        const errorBasic: ErrorBasic = {
          status: error.response.status,
          code: error.code,
          message: error.message,
        };
        if (errorBasic.status === 400) {
          setErrorName("Wrong crendiantials. Please check it");
          setShowError(true);
          toast.error("Failed to login", {
            id: loading,
          });
        }
      });
  };

  const loginToWeb = async () => {
    await login();
    const isAdmin: boolean = await CheckJWTIsAdmin();
    setIsAdmin(isAdmin);
  };

  return (
    <section className="text-gray-600 body-font bg-[#111827]">
      <div></div>
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
          {showError && <Alert message={errorName} type="error" showIcon />}
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
            onClick={loginToWeb}
            disabled={username === "" || password === ""}
            className="disabled:bg-gray-500 text-black bg-[#00df9a] border-0 py-2 px-8 focus:outline-none hover:bg-[#1c9e75] rounded text-lg font-bold"
          >
            Login
          </button>
          <div></div>
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
