import { Chips } from "primereact/chips";
import { Dispatch, SetStateAction } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  categories: string[];
  setCategories: Dispatch<SetStateAction<never[]>>;
  types: string[];
  setTypes: Dispatch<SetStateAction<never[]>>;
}

function CreateStep8({
  stepNumber,
  setStepNumber,
  categories,
  setCategories,
  types,
  setTypes,
}: Props) {
  const { previousStep, nextStep } = useWizard();

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  const inputHandleAddCategory = (value: any) => {
    setCategories(value);
  };

  const inputHandleAddTypes = (value: any) => {
    setTypes(value);
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">
          Add categories and types
        </h1>
        <p className="text-center p-2">
          Please specify the categories and types for your tabletop game. You
          must select at least one category/type, but you can add more if
          applicable. It's important to be accurate and thoughtful when choosing
          your categories/types, as providing incorrect or inappropriate
          information may result in your submission being rejected. Please
          review the available options carefully and select the most appropriate
          ones for your game.
        </p>
        <div className="mb-5">
          <div className=" font-bold flex justify-center">
            <p>Categories</p>
          </div>
        </div>
        <div>
          <div className="card p-fluid">
            <Chips
              value={categories}
              onChange={(e) => inputHandleAddCategory(e.value)}
              separator=","
            />
          </div>
        </div>
        <div className="mb-5 mt-5">
          <div className=" font-bold flex justify-center">
            <p>Types</p>
          </div>
        </div>
        <div>
          <div className="card p-fluid">
            <Chips
              value={types}
              onChange={(e) => inputHandleAddTypes(e.value)}
              separator=","
            />
          </div>
        </div>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>
        <button
          disabled={categories.length === 0 || types.length === 0}
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default CreateStep8;
