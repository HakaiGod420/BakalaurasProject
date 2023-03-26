import AcceptedInvitationsList from "../components/AcceptedInvititationsList";
import CreatedInvitationList from "../components/CreatedInvitationsList";
import EventsInvitationComponent from "../components/EventsInvitationsComponent";

function MyEvents() {
  return (
    <div className="bg-white">
      <div>
        <EventsInvitationComponent />
        <AcceptedInvitationsList />
        <CreatedInvitationList />
      </div>
    </div>
  );
}

export default MyEvents;
