import { useParams } from "react-router-dom";

function UserProfileInformation() {
  let { id } = useParams();

  return (
    <div className="bg-white">
      <div className="max-w-[1240px] flext items-center justify-center mx-auto p-10 ">
        <div>
          <Image />
        </div>
      </div>
    </div>
  );
}

export default UserProfileInformation;
