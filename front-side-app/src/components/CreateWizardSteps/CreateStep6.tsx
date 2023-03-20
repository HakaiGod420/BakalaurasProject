import { FileUpload } from "primereact/fileupload";
import { useState } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
}

function CreateStep6({ stepNumber, setStepNumber }: Props) {
  const { handleStep, previousStep, nextStep } = useWizard();

  const [rules, setRules] = useState<string>("");

  const [files, setFiles] = useState<File[]>([]);

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerSkip = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  const setImages = (value: File[]) => {
    setFiles(files.concat(value));
    console.log(files.length);
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">
          Aditional Files
        </h1>
        <p className="text-center p-2">
          Write the rules of the tabletop game you are creating now. You can
          skip this step, but others may not know how to play your game.
        </p>
        <div className="mb-5">
          <div className=" font-bold flex justify-center">
            <p>Upload Additional files</p>
          </div>
          <div>
            <div className="card">
              <FileUpload
                onUpload={(e) => setImages(e.files)}
                name="demo[]"
                multiple
                accept="file/*"
                maxFileSize={100000000}
                customUpload={true}
                emptyTemplate={
                  <p className="m-0">Drag and drop files to here to upload.</p>
                }
              />
            </div>
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

        {files?.length !== 0 ? (
          <button
            disabled={rules.length <= 100}
            className="btn m-2 min-w-[100px]"
            onClick={() => inputHandlerNext()}
          >
            Next
          </button>
        ) : (
          <button
            className="btn m-2 min-w-[100px]"
            onClick={() => inputHandlerSkip()}
          >
            Skip
          </button>
        )}
      </div>
    </div>
  );
}

export default CreateStep6;
