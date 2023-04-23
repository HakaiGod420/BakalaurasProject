import InvitationTabs from "../components/InvitationTabs";

const handleTabChange = (tab: "active" | "created" | "accepted") => {
  console.log("Selected tab:", tab);
};

function MyEvents() {
  return (
    <div className="bg-white">
      <div>
        <InvitationTabs onTabChange={handleTabChange} />
      </div>
    </div>
  );
}

export default MyEvents;
