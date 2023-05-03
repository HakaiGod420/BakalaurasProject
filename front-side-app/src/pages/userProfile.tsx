import UserCreatedTableTopGames from "../components/CreateWizardSteps/UserCreatedTableTopGames";
import UserProfileInformation from "../components/UserInformationProfile";

function UserProfile() {
  return (
    <div>
      <UserProfileInformation />
      <UserCreatedTableTopGames />
    </div>
  );
}

export default UserProfile;
