import axios from "axios";
import { useState } from "react";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { Wizard } from "react-use-wizard";
import { SERVER_API } from "../services/constants/ClienConstants";
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

  const navigate = useNavigate();

  const [title, setTitle] = useState<string>("");
  const [playerAge, setPlayerAge] = useState<number>();
  const [averageTime, setAverageTime] = useState<number | undefined>(0);
  const [playerCount, setPlayerCount] = useState<number | undefined>(0);
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

  const handleOnClose = () => {
    setTitle("");
    setPlayerAge(undefined);
    setPlayerCount(undefined);
    setAverageTime(undefined);
    setRules(undefined);
    setDescription("");
    setStetpNumber(1);
  };

  const postTableTopGame = async (tableboardgames: TabletopGameCreation) => {
    const token = JSON.parse(localStorage.getItem("token") ?? "{}");

    axios.defaults.headers.post["Authorization"] = `Bearer ${token.token}`;

    await axios
      .post(SERVER_API + "/api/gameboard/create", tableboardgames, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then((res) => {
        console.log(res);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const postImages = async (formData: FormData) => {
    await axios
      .post(SERVER_API + "/api/upload/imagePost", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      })
      .then((res) => {
        console.log(res);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const postFiles = async (formData: FormData) => {
    await axios
      .post(SERVER_API + "/api/upload/filePost", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      })
      .then((res) => {
        console.log(res);
      })
      .catch((error) => {
        console.log(error);
      });
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
    await postFiles(formDataAditionalFiles);
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

    await postImages(formData);

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

    const CreatedTableTopGame: TabletopGameCreation = {
      Title: title,
      PlayerCount: playerCount,
      PLayingAge: playerAge,
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
    await postTableTopGame(CreatedTableTopGame);
    toast.success("Successfully created game", {
      id: loading,
    });
    //navigate("/");
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
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                setDescription={setDescription}
                description={description}
              />
              <CreateStep4
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                rules={rules}
                setRules={setRules}
              />
              <CreateStep5
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                images={images}
                setImages={setImages}
                thumbnail={thumbnail}
                setThumbnail={setThumbnail}
              />
              <CreateStep8
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                categories={categories}
                setCategories={setCategories}
                types={types}
                setTypes={setTypes}
              />
              <CreateStep6
                setStepNumber={setStetpNumber}
                stepNumber={stepNumber}
                files={files}
                setFiles={setFiles}
              />
              <CreateStep7
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
