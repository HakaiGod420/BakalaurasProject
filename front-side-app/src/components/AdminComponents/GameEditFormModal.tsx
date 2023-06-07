import { AxiosError } from "axios";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import Select from "react-select";
import {
  getGameBoardForEdit,
  updateGameBoardWithNewInfo,
} from "../../services/api/GameBoardService";
import {
  EditGameBoardInfo,
  GetUpdageGameInfoResponse,
} from "../../services/types/TabletopGame";
import PhotoGaleryModal from "./PhotoGaleryModal";

type ModalProps = {
  onClose: () => void;
  gameBoardId: number | undefined;
};

type SelectOption = {
  value: string;
  label: string;
};

const GameEditFormModal: React.FC<ModalProps> = ({ onClose, gameBoardId }) => {
  const [title, setTitle] = useState("");
  const [playerCount, setPlayerCount] = useState(0);
  const [playerTime, setPlayerTime] = useState<number | undefined>(0);
  const [playableAge, setPlayableAge] = useState(0);
  const [description, setDescription] = useState("");
  const [isBlocked, setIsBlocked] = useState(false);
  const [rules, setRules] = useState<string | undefined>("");
  const [, setThumbnail] = useState<File | null>(null);
  const [additionalImages, setAdditionalImages] = useState<File[]>([]);
  const [selectedCategories, setSelectedCategories] = useState<SelectOption[]>(
    []
  );
  const [selectedTypes, setSelectedTypes] = useState<SelectOption[]>([]);
  const [allTypes, setAllTypes] = useState<SelectOption[]>([]);
  const [allCategories, setAllCategories] = useState<SelectOption[]>([]);

  const [photoGaleryIsOpen, setPhotoGaleryIsOpen] = useState(false);

  const handleThumbnailUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) setThumbnail(e.target.files[0]);
  };

  const handleAdditionalImagesUpload = (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    if (e.target.files)
      setAdditionalImages([...additionalImages, ...Array.from(e.target.files)]);
  };

  // Remember to implement the actual save logic here

  const handleSave = async () => {
    const loading = toast.loading("Saving...");
    const updateGameBoardInfo: EditGameBoardInfo = {
      GameBoardId: Number(gameBoardId),
      Title: title,
      PlayerCount: playerCount,
      PlayingTime: playerTime,
      PlayableAge: playableAge,
      Description: description,
      IsBlocked: isBlocked,
      Rules: rules,
      SelectedTypes: selectedTypes.map((option) => ({
        Value: option.value,
        Label: option.label,
      })),
      SelectedCategories: selectedCategories.map((option) => ({
        Value: option.value,
        Label: option.label,
      })),
    };
    console.log(updateGameBoardInfo);
    await updateGameBoardWithNewInfo(updateGameBoardInfo).catch(
      (error: AxiosError) => {
        toast.error(error.response?.data as string, {
          id: loading,
        });
      }
    );
    toast.success("Game board updated", { id: loading });
    onClose();
  };

  const handleSet = (response: GetUpdageGameInfoResponse) => {
    setTitle(response.EditGameBoardInfo.Title);
    setDescription(response.EditGameBoardInfo.Description);
    setPlayableAge(response.EditGameBoardInfo.PlayableAge);
    setPlayerCount(response.EditGameBoardInfo.PlayerCount);
    setIsBlocked(response.EditGameBoardInfo.IsBlocked);
    setPlayerTime(response.EditGameBoardInfo.PlayingTime);
    setRules(response.EditGameBoardInfo.Rules);
    setAllTypes(
      response.AllTypes.map((option) => ({
        value: option.Value,
        label: option.Label,
      }))
    );
    setAllCategories(
      response.AllCategories.map((option) => ({
        value: option.Value,
        label: option.Label,
      }))
    );
    setSelectedCategories(
      response.EditGameBoardInfo.SelectedCategories.map((option) => ({
        value: option.Value,
        label: option.Label,
      }))
    );
    setSelectedTypes(
      response.EditGameBoardInfo.SelectedTypes.map((option) => ({
        value: option.Value,
        label: option.Label,
      }))
    );
  };
  useEffect(() => {
    const fetchGameBoard = async () => {
      if (gameBoardId === undefined) return;
      const response = await getGameBoardForEdit(gameBoardId);
      handleSet(response);
      console.log(response);
    };
    fetchGameBoard();
  }, []);

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 overflow-auto z-50">
      <div className="bg-gray-800 rounded-lg p-6 max-w-[800px] w-[800px] h-[800px] overflow-auto">
        <h2 className="text-xl font-semibold mb-4 text-white">
          Game Board Edit Form
        </h2>

        <form>
          <label className="block text-white">
            Title:
            <input
              type="text"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              className="w-full px-2 py-1 mt-1 bg-gray-700 text-white rounded-md"
            />
          </label>
          <label className="block mt-4 text-white">
            Player Count:
            <input
              type="number"
              value={playerCount}
              onChange={(e) => setPlayerCount(parseInt(e.target.value, 10))}
              className="w-full px-2 py-1 mt-1 bg-gray-700 text-white rounded-md"
            />
          </label>
          <label className="block mt-4 text-white">
            Playing Time:
            <input
              type="number"
              value={playerTime}
              onChange={(e) => setPlayerTime(parseInt(e.target.value, 10))}
              className="w-full px-2 py-1 mt-1 bg-gray-700 text-white rounded-md"
            />
          </label>
          <label className="block mt-4 text-white">
            Playable Age:
            <input
              type="number"
              value={playableAge}
              onChange={(e) => setPlayableAge(parseInt(e.target.value, 10))}
              className="w-full px-2 py-1 mt-1 bg-gray-700 text-white rounded-md"
            />
          </label>
          <label className="block mt-4 text-white">
            Description:
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              className="w-full px-2 py-1 mt-1 bg-gray-700 text-white rounded-md"
            />
          </label>
          <label className=" mt-4 text-white flex items-center">
            Is Blocked:
            <input
              type="checkbox"
              checked={isBlocked}
              onChange={(e) => setIsBlocked(e.target.checked)}
              className="ml-2"
            />
          </label>
          <label className="block mt-4 text-white">
            Rules:
            <textarea
              value={rules}
              onChange={(e) => setRules(e.target.value)}
              className="w-full px-2 py-1 mt-1 bg-gray-700 text-white rounded-md"
            />
          </label>
          <label className="block mt-4 text-white">
            Categories:
            <Select
              className="mt-1"
              isMulti
              options={allCategories}
              value={selectedCategories}
              onChange={(selected) =>
                setSelectedCategories(selected as SelectOption[])
              }
              styles={{
                control: (provided) => ({
                  ...provided,
                  backgroundColor: "#1f2937",
                  borderRadius: "0.5rem",
                  border: "1px solid #4B5563",
                  color: "white",
                  minHeight: "3rem",
                }),
                option: (provided, state) =>
                  Object.assign({}, provided, {
                    backgroundColor: state.isSelected ? "#1D4ED8" : null,
                    color: "black",
                  }),
                input: (provided) => ({
                  ...provided,
                  color: "white",
                }),
              }}
            />
          </label>
          <label className="block mt-4 text-white">
            Types:
            <Select
              id="types"
              options={allTypes}
              isMulti
              placeholder="Types"
              value={selectedTypes}
              onChange={(selected) =>
                setSelectedTypes(selected as SelectOption[])
              }
              className="w-full"
              isClearable
              styles={{
                control: (provided) => ({
                  ...provided,
                  backgroundColor: "#1f2937",
                  borderRadius: "0.5rem",
                  border: "1px solid #4B5563",
                  color: "white",
                  minHeight: "3rem",
                }),
                option: (provided, state) =>
                  Object.assign({}, provided, {
                    backgroundColor: state.isSelected ? "#1D4ED8" : null,
                    color: "black",
                  }),
                input: (provided) => ({
                  ...provided,
                  color: "white",
                }),
              }}
            />
          </label>
        </form>
        <div className="mt-5 mb-5">
          <button
            onClick={() => setPhotoGaleryIsOpen(true)}
            className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 mr-10 w-full"
          >
            Edit photos
          </button>
        </div>

        <div className="flex justify-center mt-4">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-red-500 text-white rounded-md hover:bg-red-600 mr-10 w-[100px]"
          >
            Cancel
          </button>

          <button
            onClick={handleSave}
            className="px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 w-[100px]"
          >
            Save
          </button>
        </div>
      </div>
      {photoGaleryIsOpen && (
        <PhotoGaleryModal
          gameBoardId={gameBoardId}
          isOpen={photoGaleryIsOpen}
          onClose={() => setPhotoGaleryIsOpen(false)}
        />
      )}
    </div>
  );
};

export default GameEditFormModal;
