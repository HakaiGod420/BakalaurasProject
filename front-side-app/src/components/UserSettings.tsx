import { useEffect, useState } from "react";
import { getUserSetting } from "../services/api/UserServics";
import { UserAddress, UserSettings } from "../services/types/User";
import AddressPopup from "./Settings/AddressPopup";
import ChangeUserInformation from "./Settings/ChangeUserInformation";
import Notifications from "./Settings/NottificationSettings";
import SectionDivider from "./core/SectionDivider";
const defaultEmptyAddress: UserAddress = {
  Country: "",
  Province: "",
  City: "",
  StreetName: "",
  Map_X_Coords: undefined,
  Map_Y_Coords: undefined,
};

function UserSettingsMenu() {
  const [showAddressPopup, setShowAddressPopup] = useState(false);
  const [showNotificationPopup, setShowNotificationPopup] = useState(false);
  const [showUserInformationPopup, setShowUserInformationPopup] =
    useState(false);

  const [userSettings, setUserSettings] = useState<UserSettings>();

  useEffect(() => {
    const fetchUserSettings = async () => {
      const response = await getUserSetting();
      setUserSettings(response);
    };
    fetchUserSettings();
  }, []);

  return (
    <div className="mx-auto bg-white border-2 max-w-[1280px]">
      <div className=" rounded-lg shadow-md p-4">
        <SectionDivider label="User Settings" />
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-2 gap-4 justify-center pb-[200px]">
          <button
            onClick={() => setShowAddressPopup(true)}
            className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200"
          >
            Set up Address
          </button>
          <button
            onClick={() => setShowUserInformationPopup(true)}
            className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200"
          >
            Change profile information
          </button>
          <button
            onClick={() => setShowNotificationPopup(true)}
            className="bg-green-500 hover:bg-green-600 text-white font-medium rounded-lg py-3 px-6 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500 transition-all duration-200"
          >
            Notification Settings
          </button>
        </div>
      </div>
      {showAddressPopup && (
        <AddressPopup
          invtitationsEnbaled={userSettings?.EnabledInvitationSettings}
          onClose={() => setShowAddressPopup(false)}
          address={userSettings?.Address ?? defaultEmptyAddress}
        />
      )}
      {showNotificationPopup && (
        <Notifications
          isEnabledInviteNotifications={userSettings?.EnabledInvitationSettings}
          isOpen={showNotificationPopup}
          onClose={() => setShowNotificationPopup(false)}
        />
      )}
      {showUserInformationPopup && (
        <ChangeUserInformation
          onClose={() => setShowUserInformationPopup(false)}
        />
      )}
    </div>
  );
}

export default UserSettingsMenu;
