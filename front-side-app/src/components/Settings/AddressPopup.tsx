import React, { useState } from "react";
import { updateAddress } from "../../services/api/UserServics";
import { AddressEdit, UserAddress } from "../../services/types/User";
import SectionDivider from "../core/SectionDivider";

interface AddressPopupProps {
  address: UserAddress | undefined;
  invtitationsEnbaled: boolean | undefined;
  onClose: () => void;
}

const AddressPopup: React.FC<AddressPopupProps> = ({
  onClose,
  address,
  invtitationsEnbaled,
}) => {
  const [showMap, setShowMap] = useState(false);
  const [editAddress, setAdditableAddress] = useState<UserAddress>(address!);

  const [notificationEnabled, setNotificationEnabled] = useState<boolean>(
    invtitationsEnbaled!
  );

  const handleChange = (name: string, value: string | boolean | undefined) => {
    setAdditableAddress({
      ...editAddress,
      [name]: value,
    });
    console.log(editAddress);
  };

  const postAddress = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const newAddress: AddressEdit = {
      Address: editAddress,
      EnabledInvitationSettings: notificationEnabled,
    };
    await updateAddress(newAddress);
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-70">
      <div className="bg-white p-8 rounded-md w-96">
        <button
          onClick={onClose}
          className="absolute top-3 right-3 text-lg font-bold focus:outline-none "
        >
          &times;
        </button>
        <SectionDivider label="My Address" />
        <form onSubmit={postAddress}>
          <input
            type="text"
            placeholder="Country"
            onChange={(e) => handleChange("Country", e.target.value)}
            value={editAddress.Country}
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
          />
          <input
            type="text"
            value={editAddress.Province}
            onChange={(e) => handleChange("Province", e.target.value)}
            placeholder="Province"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
          />
          <input
            type="text"
            onChange={(e) => handleChange("City", e.target.value)}
            value={editAddress.City}
            placeholder="City"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
          />

          <input
            type="text"
            value={editAddress.StreetName}
            onChange={(e) => handleChange("StreetName", e.target.value)}
            placeholder="Street Name"
            className="w-full mb-4 px-3 py-2 border rounded-md focus:outline-none focus:border-green-500 bg-white text-black"
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
              checked={notificationEnabled}
              onChange={() => setNotificationEnabled(!notificationEnabled)}
              className="bg-white mr-2 cursor-pointer checked:accent-green-500 custom-checkbox "
            />
            <span>Enable notifications for game invitations</span>
          </label>
          <button
            disabled={
              editAddress.Country === "" ||
              editAddress.Province === "" ||
              editAddress.City === "" ||
              editAddress.StreetName === ""
            }
            className="w-full px-4 py-2 text-white bg-green-500 rounded-md hover:bg-green-600 focus:outline-none disabled:bg-gray-500"
          >
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
