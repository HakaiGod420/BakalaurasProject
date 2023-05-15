import { Alert } from "antd";
import { useState } from "react";
import toast, { Toaster } from "react-hot-toast";
import { AiOutlineCloseCircle } from "react-icons/ai";
import { Link, useNavigate } from "react-router-dom";
import { registerToSystem } from "../services/api/UserServics";
import { RegisterData } from "../services/types/User";

function RegisterForm() {
  const [email, setEmail] = useState("");
  const [usernameSetted, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [rePassword, setRePassword] = useState("");
  const [hidenErr, setErrShow] = useState(false);
  const [errorName, setErrorName] = useState("");
  const navigate = useNavigate();

  function isValidEmail(email: string) {
    return /\S+@\S+\.\S+/.test(email);
  }

  const registerToWeb = async () => {
    if (password !== rePassword) {
      setErrShow(true);
      setErrorName("Passwords must match");
      return;
    }

    if (!isValidEmail(email)) {
      setErrShow(true);
      setErrorName("Email is not valid");
      return;
    }

    const crediantals: RegisterData = {
      userName: usernameSetted,
      email: email,
      password: password,
      rePassword: rePassword,
    };

    const loading = toast.loading("Creating account");

    await registerToSystem(crediantals)
      .then(() => {
        toast.success("Successfully created account", {
          id: loading,
        });
        setErrShow(false);
        navigate("/login");
      })
      .catch(() => {
        setErrShow(true);
        toast.error("Error", {
          id: loading,
        });
      });
  };

  return (
    <section className="text-gray-600 body-font bg-[#111827]">
      <Toaster />
      <div className="container px-5 py-24 mx-auto flex flex-wrap items-center max-w-[1240px]">
        <div className="lg:w-3/5 md:w-1/2 md:pr-16 lg:pr-0 pr-0">
          <h1 className="title-font font-medium text-3xl text-[#00df9a]">
            REGISTER TO THIS STRIVING COMMUNITY TODAY
          </h1>
        </div>
        <div className="lg:w-2/6 md:w-1/2 bg-gray-100 rounded-lg p-8 flex flex-col md:ml-auto w-full mt-10 md:mt-0 border-0 border-[#1c9e75] shadow-md  shadow-[#1c9e75]">
          <h2 className="text-gray-900 text-lg font-medium title-font mb-5">
            Sign Up
          </h2>
          {hidenErr && (
            <Alert
              className="text-red-500"
              message={errorName}
              type="error"
              icon={<AiOutlineCloseCircle />}
              showIcon
            />
          )}
          <div className="mb-4">
            <label
              htmlFor="username"
              className="leading-7 text-sm text-gray-600"
            >
              Username
            </label>
            <input
              value={usernameSetted}
              onChange={(e) => setUsername(e.target.value)}
              type="username"
              id="username"
              name="username"
              className="w-full bg-white rounded border border-gray-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 text-base outline-none text-gray-700 py-1 px-3 leading-8 transition-colors duration-200 ease-in-out"
            />
          </div>

          <div className="mb-4">
            <label htmlFor="email" className="leading-7 text-sm text-gray-600">
              Email
            </label>
            <input
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              type="email"
              id="email"
              name="email"
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
          <div className="mb-4">
            <label
              htmlFor="repassword"
              className="leading-7 text-sm text-gray-600"
            >
              Reenter password
            </label>
            <input
              value={rePassword}
              onChange={(e) => setRePassword(e.target.value)}
              type="password"
              id="repassword"
              name="repassword"
              className="w-full bg-white rounded border border-gray-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 text-base outline-none text-gray-700 py-1 px-3 leading-8 transition-colors duration-200 ease-in-out"
            />
          </div>
          <button
            onClick={registerToWeb}
            disabled={
              email === "" ||
              password === "" ||
              usernameSetted === "" ||
              rePassword === ""
            }
            className="disabled:bg-gray-500 text-black bg-[#00df9a] border-0 py-2 px-8 focus:outline-none hover:bg-[#1c9e75] rounded text-lg font-bold"
          >
            Register
          </button>
          <p className="text-xs text-gray-500 mt-3">
            Already are member?{" "}
            <Link
              className=" text-[#1c9e75] hover:text-[#0e3c2d] "
              to={"/login"}
            >
              Login
            </Link>
          </p>
        </div>
      </div>
    </section>
  );
}

export default RegisterForm;
