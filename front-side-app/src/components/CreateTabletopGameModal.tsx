import { useState } from "react";
import { Wizard } from "react-use-wizard";
import { TabletopGameCreation } from "../services/types/TabletopGame";
import CreateStep1 from "./CreateWizardSteps/CreateStep1";
import CreateStep2 from "./CreateWizardSteps/CreateStep2";
import CreateStep3 from "./CreateWizardSteps/CreateStep3";
import CreateStep4 from "./CreateWizardSteps/CreateStep4";
import CreateStep5 from "./CreateWizardSteps/CreateStep5";
import CreateStep6 from "./CreateWizardSteps/CreateStep6";

function CreateTabletopGameModal() {
  const [stepNumber, setStetpNumber] = useState<number>(1);

  let TableTopGame: TabletopGameCreation = {
    Title: "",
    PlayerCount: 0,
    PLayingAge: 0,
    PlayingTime: 0,
    Description: "",
    ThumbnailName: "",
    Images: [],
    Categories: [],
    BoardTypes: [],
    AditionalFiles: [],
    SaveAsDraft: false,
  };

  const Footer = () => (
    <div className="mt-5 flex justify-center">
      <ul className="steps steps-vertical lg:steps-horizontal">
        <li className={1 <= stepNumber ? "step step-primary" : "step"}>
          Title
        </li>
        <li className={2 <= stepNumber ? "step step-primary" : "step"}>
          Players information
        </li>
        <li className={3 <= stepNumber ? "step step-primary" : "step"}>
          Description
        </li>
        <li className={4 <= stepNumber ? "step step-primary" : "step"}>
          Rules
        </li>
        <li className={5 <= stepNumber ? "step step-primary" : "step"}>
          Images
        </li>
        <li className={6 <= stepNumber ? "step step-primary" : "step"}>
          Additional files
        </li>
      </ul>
    </div>
  );

  const handleOnClose = () => {
    setStetpNumber(1);
  };

  return (
    <div className="">
      <label
        htmlFor="my-modal-5"
        className="btn min-h-[70px] min-w-full btn-active btn-primary no-animation hover:bg-green-900"
      >
        Share your favorite a table board game
      </label>

      {/* Put this part before </body> tag */}
      <input type="checkbox" id="my-modal-5" className="modal-toggle" />
      <div className="modal">
        <div className="modal-box w-11/12 max-w-5xl">
          <label
            onClick={handleOnClose}
            htmlFor="my-modal-5"
            className="btn btn-sm btn-circle absolute right-2 top-2"
          >
            ✕
          </label>
          <div>
            <Wizard startIndex={0} footer={<Footer />}>
              <CreateStep1
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                tableTopGame={TableTopGame}
              />
              <CreateStep2
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                tableTopGame={TableTopGame}
              />
              <CreateStep3
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
              />
              <CreateStep4
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
              />
              <CreateStep5
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
              />
              <CreateStep6
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

export default CreateTabletopGameModal;
