import React, { useState } from "react";
import AcceptedInvitationsList from "./AcceptedInvititationsList";
import CreatedInvitationList from "./CreatedInvitationsList";
import EventsInvitationComponent from "./EventsInvitationsComponent";

type Tab = "active" | "created" | "accepted";

interface InvitationTabsProps {
  onTabChange: (tab: Tab) => void;
}

const InvitationTabs: React.FC<InvitationTabsProps> = ({ onTabChange }) => {
  const [activeTab, setActiveTab] = useState<Tab>("active");

  const handleTabClick = (tab: Tab) => {
    setActiveTab(tab);
    onTabChange(tab);
  };

  const renderTab = (tab: Tab, label: string) => {
    const isActive = activeTab === tab;
    const baseClasses = "px-6 py-4 cursor-pointer text-lg mt-5";
    const activeClasses = "bg-gray-300 text-green-700 font-semibold";
    const inactiveClasses = "text-gray-600 hover:bg-gray-200";

    return (
      <div className="text-center">
        <div
          onClick={() => handleTabClick(tab)}
          className={`${baseClasses} ${
            isActive ? activeClasses : inactiveClasses
          }`}
        >
          {label}
        </div>
      </div>
    );
  };

  return (
    <div className="bg-white shadow rounded-md">
      <div className="flex justify-center">
        {renderTab("active", "Active Invitation")}
        {renderTab("created", "Created Invitation")}
        {renderTab("accepted", "Accepted Invitation")}
      </div>
      {activeTab === "active" && (
        <div>
          <EventsInvitationComponent />
        </div>
      )}
      {activeTab === "accepted" && (
        <div>
          {" "}
          <AcceptedInvitationsList />
        </div>
      )}
      {activeTab === "created" && (
        <div>
          {" "}
          <CreatedInvitationList />
        </div>
      )}
    </div>
  );
};

export default InvitationTabs;
