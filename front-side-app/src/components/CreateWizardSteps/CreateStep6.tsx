import { FileUpload, FileUploadHandlerEvent } from "primereact/fileupload";
import { useState } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  files: File[];
  setFiles: React.Dispatch<React.SetStateAction<File[]>>;
}

function CreateStep6({ stepNumber, setStepNumber, files, setFiles }: Props) {
  const { handleStep, previousStep, nextStep } = useWizard();

  const [rules, setRules] = useState<string>("");

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

  const Upload = (event: FileUploadHandlerEvent) => {
    const preFiles: File[] = [];
    event.files.forEach((file) => {
      preFiles.push(file);
    });
    console.log(preFiles);
    setFiles(preFiles);
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">
          Aditional Files
        </h1>
        <p className="text-center p-2">
          You can also attach additional files containing interesting facts
          about tabletop games, strategies, additional rules, and more. This
          step is optional, so you may choose to skip it.
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
                auto
                accept="image/*png"
                maxFileSize={100000000}
                customUpload={true}
                uploadHandler={Upload}
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
            disabled={files.length > 0 ? false : true}
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
