import { useState } from "react";
import { Wizard } from "react-use-wizard";
import InviteStep1 from "./InviteWizardSteps/InviteStep1";
import InviteStep2 from "./InviteWizardSteps/InviteStep2";
import InviteStep3 from "./InviteWizardSteps/InviteStep3";
import InviteStep4 from "./InviteWizardSteps/InviteStep4";

function CreateInvitationModal() {
  const [stepNumber, setStetpNumber] = useState<number>(1);

  const [title, setTitle] = useState<string>("");

  const Footer = () => (
    <div className="mt-5 flex justify-center">
      <ul className="steps steps-vertical lg:steps-horizontal">
        <li
          className={1 <= stepNumber ? "mr-2 step step-primary" : "mr-2 step"}
        >
          Find game
        </li>
        <li
          className={2 <= stepNumber ? "mr-2 step step-primary" : " mr-2 step"}
        >
          Parcipants
        </li>
        <li
          className={3 <= stepNumber ? "mr-2 step step-primary" : " mr-2 step"}
        >
          Address
        </li>
        <li
          className={4 <= stepNumber ? "mr-2 step step-primary" : " mr-2 step"}
        >
          Finish
        </li>
      </ul>
    </div>
  );

  const handleOnClose = () => {
    setTitle("");
    setStetpNumber(1);
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
              />
              <InviteStep3
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
              />
              <InviteStep4
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
              />
            </Wizard>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CreateInvitationModal;
