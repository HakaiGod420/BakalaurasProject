import { Dayjs } from "dayjs";
import { useState } from "react";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { Wizard } from "react-use-wizard";
import { useRecoilState } from "recoil";
import { postInvitation } from "../services/api/InvitationService";
import { selectedGameBoard } from "../services/constants/recoil/gameboardStates";
import { Address, Invitation } from "../services/types/Invitation";
import { MapCoordinates } from "../services/types/Miscellaneous";
import InviteStep1 from "./InviteWizardSteps/InviteStep1";
import InviteStep2 from "./InviteWizardSteps/InviteStep2";
import InviteStep3 from "./InviteWizardSteps/InviteStep3";
import InviteStep4 from "./InviteWizardSteps/InviteStep4";

const defaultCords: MapCoordinates = {
  Lat: 0,
  Lng: 0,
};

const DefaultAddress: Address = {
  Country: "",
  PostalCode: "",
  City: "",
  StreetName: "",
  HouseNumber: undefined,
  Province: "",
};

function CreateInvitationModal() {
  const [stepNumber, setStetpNumber] = useState<number>(1);
  const navigate = useNavigate();

  const [title, setTitle] = useState<string>("");
  const [maxParcipants, setMaxParcipants] = useState<number>();
  const [minimalAge, setMinimalAge] = useState<number>();
  const [mapCoords, setMapCoords] = useState<MapCoordinates>(defaultCords);
  const [date, setDate] = useState<Dayjs | null>();
  const [selectedGame] = useRecoilState(selectedGameBoard);
  const [address, setAddress] = useState<Address>(DefaultAddress);
  const [resetWizard, setResetWizard] = useState<boolean>(false);

  const Footer = () => (
    <div className="mt-5 flex justify-center">
      <ul className="steps steps-vertical lg:steps-horizontal w-[500px]">
        <li className={1 <= stepNumber ? "step step-primary" : "step"}>
          Find game
        </li>
        <li className={2 <= stepNumber ? "step step-primary" : "step"}>
          Parcipants
        </li>
        <li className={3 <= stepNumber ? "step step-primary" : "step"}>
          Address
        </li>
        <li className={4 <= stepNumber ? "step step-primary" : "step"}>
          Finish
        </li>
      </ul>
    </div>
  );

  const handleOnClose = () => {
    setTitle("");
    setStetpNumber(0);
  };

  const publishInvitation = async () => {
    const loading = toast.loading("Creating invitation...");
    const Invitation: Invitation = {
      ActiveGameId: Number(selectedGame?.Id) ?? -1,
      Address: address,
      InvitationDate: date?.format("YYYY-MM-DDTHH:MM") ?? "",
      Map_X_Cords: mapCoords.Lat,
      Map_Y_Cords: mapCoords.Lng,
      PlayersNeeded: maxParcipants ?? 0,
      MinimalAge: minimalAge ?? 0,
    };

    await postInvitation(Invitation).catch((err) => {
      toast.error("Error", {
        id: loading,
      });
      return;
    });
    toast.success("Successfully created", {
      id: loading,
    });

    await Promise.resolve(setResetWizard(true));
    await Promise.resolve(setResetWizard(false));

    navigate("/myeventes/created");
  };
  return (
    <div className="">
      <label
        htmlFor="my-modal-6"
        className="btn min-h-[70px] min-w-full btn-active btn-primary no-animation hover:bg-green-900"
      >
        Create an open invitation to join a tabletop game
      </label>

      {/* Put this part before </body> tag */}
      <input
        type="checkbox"
        id="my-modal-6"
        className="modal-toggle scrollbar-hide"
      />
      <div className="modal">
        <div className="modal-box w-11/12 max-w-5xl">
          <label
            onClick={handleOnClose}
            htmlFor="my-modal-6"
            className="btn btn-sm btn-circle absolute right-2 top-2"
          >
            ✕
          </label>
          <div>
            <Wizard
              startIndex={0}
              header={
                <h1 className="font-bold uppercase">
                  TABKE TOP GAME INVITATIION
                </h1>
              }
              footer={<Footer />}
            >
              <InviteStep1
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                title={title}
                setTitle={setTitle}
              />
              <InviteStep2
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                parcipantsCount={maxParcipants}
                setParcipantsCount={setMaxParcipants}
                minimalParcipantsAge={minimalAge}
                setMinimalParcipantsAge={setMinimalAge}
              />
              <InviteStep3
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                mapCoords={mapCoords}
                setMapsCoords={setMapCoords}
                address={address}
                setAddress={setAddress}
              />
              <InviteStep4
                resetWizard={resetWizard}
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                date={date}
                setDate={setDate}
                finishMethod={publishInvitation}
              />
            </Wizard>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CreateInvitationModal;
