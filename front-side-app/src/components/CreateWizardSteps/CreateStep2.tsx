import { useWizard } from "react-use-wizard";

function CreateStep2() {
  const { handleStep, previousStep, nextStep } = useWizard();

  // Attach an optional handler
  handleStep(() => {
    alert("Going to step 2");
  });

  return (
    <div>
      <div>SAD NOSES 2</div>
      <div className="p-2 m-1">
        <button className="btn m-2" onClick={() => previousStep()}>
          Previous ⏮️
        </button>
        <button className="btn m-2" onClick={() => nextStep()}>
          Next ⏭
        </button>
      </div>
    </div>
  );
}

export default CreateStep2;
