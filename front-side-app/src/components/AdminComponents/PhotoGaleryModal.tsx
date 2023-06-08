import React, { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { FaTimes } from "react-icons/fa";
import { deleteImage, getGalleryItems } from "../../services/api/AdminService";
import { SERVER_API } from "../../services/constants/ClienConstants";
import { GalleryItem } from "../../services/types/TabletopGame";

interface PhotoGalleryModalProps {
  isOpen: boolean;
  onClose: () => void;
  gameBoardId: number | undefined;
}

const PhotoGalleryModal: React.FC<PhotoGalleryModalProps> = ({
  isOpen,
  onClose,
  gameBoardId,
}) => {
  const [selectedPhoto, setSelectedPhoto] = useState<number | null>(null);
  const [gallery, setGallery] = useState<GalleryItem[]>([]);
  const [refetch, setRefect] = useState<boolean>(false);

  const handlePhotoClick = (photo: number) => {
    setSelectedPhoto((prevSelectedPhoto) =>
      prevSelectedPhoto === photo ? null : photo
    );
  };

  const handleDelete = async (photo: number) => {
    const loading = toast.loading("Deleting photo...");
    await deleteImage(photo).catch((error) => {
      console.log(error);
      toast.error("Failed to delete photo", { id: loading });
    });
    toast.success("Photo deleted successfully", { id: loading });
    setRefect(!refetch);
  };

  useEffect(() => {
    const getGallery = async () => {
      const response = await getGalleryItems(gameBoardId as number);
      console.log(response);
      setGallery(response.GalleryElements);
    };
    getGallery();
  }, [gameBoardId, refetch]);

  return (
    <div
      className={`fixed inset-0 flex items-center justify-center ${
        isOpen ? "visible" : "hidden"
      } bg-gray-800 bg-opacity-50`}
    >
      <div className="bg-gray-700 w-1/2 rounded-lg shadow-lg p-4 h-auto">
        <div className="flex justify-between mb-4">
          <h2 className="text-white text-2xl font-bold">Photo Gallery</h2>
          <button
            className="text-gray-500 hover:text-gray-300"
            onClick={onClose}
          >
            <FaTimes className="h-6 w-6" />
          </button>
        </div>
        <div className="overflow-y-auto max-h-96">
          <div className="grid grid-cols-3 gap-4">
            {gallery.map((photo, index) => (
              <div key={index} className="relative">
                <img
                  src={SERVER_API + "/" + photo.Location}
                  alt={photo.ImageId.toString()}
                  className={`cursor-pointer rounded-lg w-full h-40 object-cover ${
                    selectedPhoto === photo.ImageId
                      ? "ring-4 ring-blue-500"
                      : ""
                  }`}
                  onClick={() => handlePhotoClick(photo.ImageId)}
                />
                {selectedPhoto === photo.ImageId && (
                  <button
                    className="absolute bottom-0 right-0 px-2 py-1 bg-red-500 text-white text-sm rounded-bl-lg"
                    onClick={() => handleDelete(photo.ImageId)}
                  >
                    Delete
                  </button>
                )}
              </div>
            ))}
          </div>
        </div>
        {gallery.length === 0 && (
          <div className="flex justify-center items-center text-[25px] text-center p-10 font-bold text-gray-400">
            No images was found
          </div>
        )}
      </div>
    </div>
  );
};

export default PhotoGalleryModal;
