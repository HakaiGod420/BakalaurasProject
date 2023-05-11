import { useState } from "react";
import Select from "react-select";

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
  const [playerTime, setPlayerTime] = useState(0);
  const [playableAge, setPlayableAge] = useState(0);
  const [description, setDescription] = useState("");
  const [isBlocked, setIsBlocked] = useState(false);
  const [rules, setRules] = useState("");
  const [thumbnail, setThumbnail] = useState<File | null>(null);
  const [additionalImages, setAdditionalImages] = useState<File[]>([]);
  const [tags, setTags] = useState<SelectOption[]>([]);
  const [types, setTypes] = useState<SelectOption[]>([]);

  // Here you can put your categories and types for the Select component
  const categories: SelectOption[] = [];
  const typesData: SelectOption[] = [];

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
  const handleSave = () => {
    onClose();
  };

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
            Player Time:
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
          <label className="block mt-4 text-white flex items-center">
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
            Thumbnail:
            <input
              type="file"
              onChange={handleThumbnailUpload}
              className="w-full px-2 py-1 mt-1 bg-gray-700 text-white rounded-md"
            />
          </label>
          <label className="block mt-4 text-white">
            Additional Images:
            <input
              type="file"
              multiple
              onChange={handleAdditionalImagesUpload}
              className="w-full px-2 py-1 mt-1 bg-gray-700 text-white rounded-md"
            />
          </label>
          <label className="block mt-4 text-white">
            Tags:
            <Select
              className="mt-1"
              isMulti
              options={categories}
              value={tags}
              onChange={(selected) => setTags(selected as SelectOption[])}
            />
          </label>
          <label className="block mt-4 text-white">
            Types:
            <Select
              id="types"
              options={types}
              isMulti
              placeholder="Types"
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
    </div>
  );
};

export default GameEditFormModal;
