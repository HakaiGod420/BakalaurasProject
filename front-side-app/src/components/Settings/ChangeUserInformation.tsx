import "leaflet/dist/leaflet.css";
import React, { useState } from "react";
import { FaExclamationCircle } from "react-icons/fa";
import { RiLockPasswordFill } from "react-icons/ri";
import { changeUserInformation } from "../../services/api/UserServics";
import { ChangedUserInformation } from "../../services/types/User";
import SectionDivider from "../core/SectionDivider";

interface UserInformationProps {
  onClose: () => void;
}

const ChangeUserInformation: React.FC<UserInformationProps> = ({ onClose }) => {
  const [changePassword, setChangePassword] = useState(false);

  const [error, setError] = useState<string>("");
  const [showError, setShowError] = useState<boolean>(false);

  const [email, setEmail] = useState<string>("");
  const [password, setPassworld] = useState<string>("");
  const [oldPassword, setOldPassword] = useState<string>("");
  const [rePassword, setRePassword] = useState<string>("");

  const togglePassword = () => setChangePassword(!changePassword);

  const validateEmail = (value: string) => {
    if (!value) {
      return "Email is required";
    }
    if (!/\S+@\S+\.\S+/.test(value)) {
      return "Email is invalid";
    }
    return "";
  };

  const validatePassword = () => {
    if (password !== rePassword) {
      return "Passwords do not match";
    }
    return "";
  };

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const emailCheck = validateEmail(email);

    if (emailCheck !== "") {
      setError(emailCheck);
      setShowError(true);
      return;
    }

    const passwordCheck = validatePassword();

    if (passwordCheck !== "" && changePassword) {
      setError(passwordCheck);
      setShowError(true);
      return;
    }

    const userChangeInfomration: ChangedUserInformation = {
      Email: email,
      PasswordChanged: changePassword,
      OldPassword: oldPassword,
      NewPassword: password,
    };

    const response = (await changeUserInformation(
      userChangeInfomration
    )) as any;

    console.log(response);

    if (response.response.status === 200) {
      setShowError(false);
    } else {
      setError("Old password wrong  ");
      setShowError(true);
    }
  };
  return (
    <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-70">
      <div className="bg-white p-8 rounded-md w-96">
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-lg font-bold focus:outline-none"
        >
          &times;
        </button>
        <SectionDivider label="Edit user information" />
        {showError && (
          <div className="bg-red-400 p-2 m-2 text-black rounded-md flex items-center">
            <FaExclamationCircle className="mr-2 h-5 w-5" />
            <span>{error}</span>
          </div>
        )}
        <form onSubmit={onSubmit}>
          <div className="mb-4">
            <input
              type="email"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="w-full px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
            />
          </div>
          <div className="flex items-center mb-4">
            <button
              type="button"
              className={`${
                changePassword ? "bg-green-500" : "bg-gray-200"
              } relative inline-flex items-center justify-center h-6 rounded-full w-11 transition-colors duration-200 focus:outline-none mr-5`}
              onClick={togglePassword}
            >
              <span
                className={`${
                  changePassword ? "translate-x-3" : "translate-x-[-10px]"
                } inline-block w-4 h-4 transform bg-white rounded-full transition-transform duration-200 ease-in-out`}
              />
            </button>
            <div className="flex items-center mr-4">
              <RiLockPasswordFill className="mr-2 text-black" />
              <span className="text-sm font-medium text-black">
                Change password
              </span>
            </div>
          </div>
          {changePassword ? (
            <>
              <div className="mb-4">
                <input
                  value={oldPassword}
                  onChange={(e) => setOldPassword(e.target.value)}
                  type="password"
                  placeholder="Current Password"
                  className="w-full px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
                />
              </div>
              <div className="mb-4">
                <input
                  value={password}
                  onChange={(e) => setPassworld(e.target.value)}
                  type="password"
                  placeholder="New Password"
                  className="w-full px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
                />
              </div>
              <div className="mb-4">
                <input
                  value={rePassword}
                  onChange={(e) => setRePassword(e.target.value)}
                  type="password"
                  placeholder="Re-Enter New Password"
                  className="w-full px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
                />
              </div>
            </>
          ) : null}
          <button className="w-full px-4 py-2 text-white bg-green-500 rounded-md hover:bg-green-600 focus:outline-none disabled:bg-gray-500">
            Submit
          </button>
        </form>
        <button
          onClick={onClose}
          className="w-full px-4 py-2 text-white bg-red-500 mt-3 rounded-md hover:bg-red-600 focus:outline-none"
        >
          Discard changes
        </button>
      </div>
    </div>
  );
};

export default ChangeUserInformation;
