import React, { useState } from "react";
import SectionDivider from "../core/SectionDivider";

interface AddressPopupProps {
  onClose: () => void;
}

const AddressPopup: React.FC<AddressPopupProps> = ({ onClose }) => {
  const [showMap, setShowMap] = useState(false);
  const [sendNotifications, setSendNotifications] = useState(false);

  return (
    <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-50">
      <div className="bg-white p-8 rounded-md w-96">
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-lg font-bold focus:outline-none "
        >
          &times;
        </button>
        <SectionDivider label="My Address" />
        <form>
          <input
            type="text"
            placeholder="City"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-blue-500"
          />
          <input
            type="text"
            placeholder="Street Name"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-blue-500"
          />
          <input
            type="text"
            placeholder="Province"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-blue-500"
          />
          <label className="flex items-center cursor-pointer mb-4 text-black">
            <input
              type="checkbox"
              onChange={() => setShowMap(!showMap)}
              className="mr-2 cursor-pointer accent-green-500"
            />
            <span>Add your location in map</span>
          </label>
          {showMap && (
            <div className="h-64 w-full mb-4 border rounded-md">
              Map implementation
            </div>
          )}
          <label className="flex items-center cursor-pointer mb-4 text-black">
            <input
              type="checkbox"
              onChange={() => setSendNotifications(!sendNotifications)}
              className="mr-2 cursor-pointer accent-green-500"
            />
            <span>Enable notifications</span>
          </label>
          <button className="w-full px-4 py-2 text-white bg-green-500 rounded-md hover:bg-green-600 focus:outline-none">
            Submit
          </button>
          <button
            onClick={onClose}
            className="w-full px-4 py-2 text-white bg-red-500 mt-3 rounded-md hover:bg-red-600 focus:outline-none"
          >
            Discard changes
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddressPopup;
