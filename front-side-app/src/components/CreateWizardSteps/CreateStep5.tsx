import { FileUpload, FileUploadHandlerEvent } from "primereact/fileupload";
import { useEffect } from "react";
import { useWizard } from "react-use-wizard";

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;
  images: File[];
  setImages: React.Dispatch<React.SetStateAction<File[]>>;
  thumbnail: File | undefined;
  setThumbnail: React.Dispatch<React.SetStateAction<File | undefined>>;
  resetWizard: boolean;
}

function CreateStep5({
  stepNumber,
  setStepNumber,
  images,
  setImages,
  thumbnail,
  setThumbnail,
  resetWizard,
}: Props) {
  const { previousStep, nextStep, goToStep } = useWizard();

  const inputHandlerNext = () => {
    setStepNumber(stepNumber + 1);
    nextStep();
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  const Upload = (event: FileUploadHandlerEvent) => {
    const preFiles: File[] = [];
    event.files.forEach((file) => {
      preFiles.push(file);
    });
    setImages(preFiles);
  };

  const UploadThumbail = (event: FileUploadHandlerEvent) => {
    const preFiles: File[] = [];
    event.files.forEach((file) => {
      preFiles.push(file);
      console.log(file);
    });
    setThumbnail(preFiles[0]);
  };

  useEffect(() => {
    if (resetWizard) {
      goToStep(0);
      console.log("resetWizard");
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [resetWizard]);

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">Images</h1>
        <p className=" text-center p-2">
          Next, you'll need to add pictures to your submission. You can include
          a thumbnail image for your tabletop game, as well as multiple
          additional pictures to showcase gameplay, components, or other
          relevant aspects of your game. While adding pictures is optional, we
          highly recommend doing so to increase the chances of your submission
          being approved. Without pictures, the approval process may take longer
          or even result in rejection.
        </p>
        <div className="mb-5">
          <div className=" font-bold flex justify-center">
            <p>Upload Thumbnail</p>
          </div>
          <div className="card">
            <FileUpload
              name="demo[]"
              multiple={false}
              auto
              accept="image/*"
              maxFileSize={100000000}
              customUpload={true}
              uploadHandler={UploadThumbail}
              emptyTemplate={
                <p className="m-0">
                  Drag and drop files to here to upload thumbnail.
                </p>
              }
            />
          </div>
        </div>
        <div className="mb-5">
          <div className=" font-bold flex justify-center">
            <p>Upload Images</p>
          </div>
          <div className="card">
            <FileUpload
              name="demo[]"
              multiple={true}
              auto
              accept="image/*"
              maxFileSize={100000000}
              customUpload={true}
              uploadHandler={Upload}
              emptyTemplate={
                <p className="m-0">
                  Drag and drop files to here to upload images.
                </p>
              }
            />
          </div>
        </div>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>

        <button
          disabled={images.length <= 0 || thumbnail === undefined}
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerNext()}
        >
          Next
        </button>
      </div>
    </div>
  );
}

export default CreateStep5;
