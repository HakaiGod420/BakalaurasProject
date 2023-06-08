import { useState } from "react";
import toast from "react-hot-toast";
import { Wizard } from "react-use-wizard";
import {
  postFiles,
  postImages,
  postTableTopGame,
} from "../services/api/GameBoardService";
import {
  AditionalFile,
  BoardType,
  Category,
  Image,
  TabletopGameCreation,
} from "../services/types/TabletopGame";
import CreateStep1 from "./CreateWizardSteps/CreateStep1";
import CreateStep2 from "./CreateWizardSteps/CreateStep2";
import CreateStep3 from "./CreateWizardSteps/CreateStep3";
import CreateStep4 from "./CreateWizardSteps/CreateStep4";
import CreateStep5 from "./CreateWizardSteps/CreateStep5";
import CreateStep6 from "./CreateWizardSteps/CreateStep6";
import CreateStep7 from "./CreateWizardSteps/CreateStep7";
import CreateStep8 from "./CreateWizardSteps/CreateStep8";

function CreateTabletopGameModal() {
  const [stepNumber, setStetpNumber] = useState<number>(1);

  const [resetWizard, setResetWizard] = useState<boolean>(false);

  const [title, setTitle] = useState<string>("");
  const [playerAge, setPlayerAge] = useState<number>();
  const [averageTime, setAverageTime] = useState<number | undefined>();
  const [playerCount, setPlayerCount] = useState<number | undefined>();
  const [description, setDescription] = useState<string>("");
  const [rules, setRules] = useState<string | undefined>();

  const [categories, setCategories] = useState([]);
  const [types, setTypes] = useState([]);

  const [files, setFiles] = useState<File[]>([]);
  const [images, setImages] = useState<File[]>([]);
  const [thumbnail, setThumbnail] = useState<File>();

  let TableTopGame: TabletopGameCreation = {
    Title: "",
    PlayerCount: 0,
    PlayableAge: 0,
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
          Tags and Categories
        </li>
        <li className={7 <= stepNumber ? "step step-primary" : "step"}>
          Additional files
        </li>
        <li className={8 <= stepNumber ? "step step-primary" : "step"}>
          Finish
        </li>
      </ul>
    </div>
  );

  const handleOnClose = async () => {
    console.log("heres");
    setTitle("");
    setPlayerAge(undefined);
    setPlayerCount(undefined);
    setAverageTime(undefined);
    setRules(undefined);
    setDescription("");
    setStetpNumber(1);
    await Promise.resolve(setResetWizard(true));
    await Promise.resolve(setResetWizard(false));
  };

  const postAdditionalFiles = async () => {
    const additionalFiles: AditionalFile[] = [];
    const formDataAditionalFiles = new FormData();
    let counter = 1;
    files.forEach((file) => {
      const newFileName =
        counter.toString() +
        "_" +
        title.replace(/[^A-Z0-9]+/gi, "_") +
        "." +
        file.type.split("/")[1];
      const location = "AditionalFiles/" + title.replace(/[^A-Z0-9]+/gi, "_");
      formDataAditionalFiles.append("fileNames", newFileName);
      formDataAditionalFiles.append("files", file);
      additionalFiles.push({ Name: newFileName, Location: location });
      counter++;
    });
    formDataAditionalFiles.append(
      "tabletopTitle",
      title.replace(/[^A-Z0-9]+/gi, "_")
    );

    console.log(files);
    await postFiles(formDataAditionalFiles).catch((error) => {
      console.log(error);
    });
    return additionalFiles;
  };

  const uploadImages = async () => {
    const selectedImages: Image[] = [];
    const formData = new FormData();
    let counter = 1;
    images.forEach((image) => {
      const newFileName =
        counter.toString() +
        "_" +
        title.replace(/[^A-Z0-9]+/gi, "_") +
        "." +
        image.type.split("/")[1];
      const location = "Images/" + title.replace(/[^A-Z0-9]+/gi, "_");
      formData.append("fileNames", newFileName);
      formData.append("images", image);
      selectedImages.push({ Alias: newFileName, Location: location });
      counter++;
    });

    formData.append("tabletopTitle", title.replace(/[^A-Z0-9]+/gi, "_"));

    await postImages(formData).catch((error) => {
      console.log(error);
    });

    return selectedImages;
  };

  const uploadThumbnail = async () => {
    let thumnailName: string = "";
    const formData = new FormData();

    const newFileName =
      "Thumbnail" +
      "_" +
      title.replace(/[^A-Z0-9]+/gi, "_") +
      "." +
      thumbnail?.type.split("/")[1];
    formData.append("fileNames", newFileName);
    formData.append("images", thumbnail!);
    thumnailName = newFileName;

    formData.append("tabletopTitle", title.replace(/[^A-Z0-9]+/gi, "_"));

    await postImages(formData);

    return thumnailName;
  };

  const publishTableTopGame = async () => {
    try {
      const loading = toast.loading("Creating your game...");

      const categoriesMapped: Category[] = [];
      const typesMapped: BoardType[] = [];

      categories.forEach((category) => {
        categoriesMapped.push({ CategoryName: category });
      });

      types.forEach((type) => {
        typesMapped.push({ BoardTypeName: type });
      });

      let uploadedAdditionalFiles: AditionalFile[] = [];
      if (files.length !== 0) {
        uploadedAdditionalFiles = await postAdditionalFiles();
      }

      let uploadedImages: Image[] = [];

      if (images.length !== 0) {
        uploadedImages = await uploadImages();
      }

      let uploadedThumbnailName: string = "";

      if (thumbnail !== undefined) {
        uploadedThumbnailName = await uploadThumbnail();
      }
      console.log(playerAge);

      const CreatedTableTopGame: TabletopGameCreation = {
        Title: title,
        PlayerCount: playerCount,
        PlayableAge: playerAge,
        PlayingTime: averageTime,
        Description: description,
        Images: uploadedImages,
        Categories: categoriesMapped,
        BoardTypes: typesMapped,
        AditionalFiles: uploadedAdditionalFiles,
        SaveAsDraft: false,
        ThumbnailName: uploadedThumbnailName,
      };

      console.log(CreatedTableTopGame);

      await postTableTopGame(CreatedTableTopGame).catch((error) => {
        toast.error("Failed to create game", { id: loading });
        return;
      });

      toast.success("Successfully created game", {
        id: loading,
      });
      const object = document.getElementById("my-modal-5")! as HTMLInputElement;
      object.checked = false;
      handleOnClose();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="">
      <label
        htmlFor="my-modal-5"
        className="btn min-h-[70px] min-w-full btn-active btn-primary no-animation hover:bg-green-900"
      >
        Share your favorite table board game here
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
            <Wizard
              startIndex={0}
              header={
                <h1 className="font-bold uppercase">TABLE TOP GAME CREATION</h1>
              }
              footer={<Footer />}
            >
              <CreateStep1
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                title={title}
                setTitle={setTitle}
              />
              <CreateStep2
                resetWizard={resetWizard}
                setAge={setPlayerAge}
                playerAge={playerAge}
                setAverageTime={setAverageTime}
                averageTime={averageTime}
                setPlayerCount={setPlayerCount}
                playerCount={playerCount}
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                tableTopGame={TableTopGame}
              />
              <CreateStep3
                resetWizard={resetWizard}
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                setDescription={setDescription}
                description={description}
              />
              <CreateStep4
                resetWizard={resetWizard}
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                rules={rules}
                setRules={setRules}
              />
              <CreateStep5
                resetWizard={resetWizard}
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                images={images}
                setImages={setImages}
                thumbnail={thumbnail}
                setThumbnail={setThumbnail}
              />
              <CreateStep8
                resetWizard={resetWizard}
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                categories={categories}
                setCategories={setCategories}
                types={types}
                setTypes={setTypes}
              />
              <CreateStep6
                resetWizard={resetWizard}
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                files={files}
                setFiles={setFiles}
              />
              <CreateStep7
                resetWizard={resetWizard}
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                publishTabletopGame={publishTableTopGame}
              />
            </Wizard>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CreateTabletopGameModal;
