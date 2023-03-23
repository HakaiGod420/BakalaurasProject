import { useState } from "react";
import SectionDivider from "./core/SectionDivider";
import AddressPopup from "./Settings/AddressPopup";
import NotificationSettings from "./Settings/NottificationSettings";

function UserSettings() {
  const [showAddressPopup, setShowAddressPopup] = useState(false);
  const [showNotificationPopup, setShowNotificationPopup] = useState(false);

  return (
    <div className="mx-auto bg-white border-2 max-w-[1280px]">
      <div className=" rounded-lg shadow-md p-4">
        <SectionDivider label="User Settings" />
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 justify-center">
          <button
            onClick={() => setShowAddressPopup(true)}
            className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200"
          >
            Set up Address
          </button>
          <button className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200">
            Privacy Settings
          </button>
          <button
            onClick={() => setShowNotificationPopup(true)}
            className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200"
          >
            Notification Settings
          </button>
          <button className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200">
            Billing Settings
          </button>
          <button className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200">
            Security Settings
          </button>
          <button className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200">
            Help & Support
          </button>
        </div>
      </div>
      {showAddressPopup && (
        <AddressPopup onClose={() => setShowAddressPopup(false)} />
      )}
      {showNotificationPopup && (
        <NotificationSettings
          isOpen={showNotificationPopup}
          onClose={() => setShowNotificationPopup(false)}
        />
      )}
    </div>
  );
}

export default UserSettings;
