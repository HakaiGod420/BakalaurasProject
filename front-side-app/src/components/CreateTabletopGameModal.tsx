import { Wizard } from "react-use-wizard";
import CreateStep1 from "./CreateWizardSteps/CreateStep1";
import CreateStep2 from "./CreateWizardSteps/CreateStep2";

const Footer = () => (
  <div className="mt-5 flex justify-center">
    <ul className="steps steps-vertical lg:steps-horizontal">
      <li className="step step-primary">Title</li>
      <li className="step">Player information</li>
      <li className="step">Description</li>
      <li className="step">Rules</li>
      <li className="step">Additional files</li>
    </ul>
  </div>
);

function CreateTabletopGameModal() {
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
            htmlFor="my-modal-5"
            className="btn btn-sm btn-circle absolute right-2 top-2"
          >
            ✕
          </label>
          <div>
            <Wizard footer={<Footer />}>
              <CreateStep1 />
              <CreateStep2 />
            </Wizard>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CreateTabletopGameModal;
