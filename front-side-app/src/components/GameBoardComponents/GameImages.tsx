import { motion } from "framer-motion";
import { useState } from "react";
import { FaArrowLeft, FaArrowRight, FaTimes } from "react-icons/fa";
import { SERVER_API } from "../../services/constants/ClienConstants";
import { TabletopImage } from "../../services/types/TabletopGame";

interface GameImagesProps {
  images: TabletopImage[];
}

const GameImages = ({ images }: GameImagesProps) => {
  const [selectedImageIndex, setSelectedImageIndex] = useState<number>(0);
  const [isFullscreen, setIsFullscreen] = useState<boolean>(false);

  const handleImageClick = (index: number) => {
    setSelectedImageIndex(index);
    setIsFullscreen(true);
  };

  const handleFullscreenClose = () => {
    setIsFullscreen(false);
  };

  const handleFullscreenNext = () => {
    setSelectedImageIndex((prevIndex) =>
      prevIndex === images.length - 1 ? 0 : prevIndex + 1
    );
  };

  const handleFullscreenPrev = () => {
    setSelectedImageIndex((prevIndex) =>
      prevIndex === 0 ? images.length - 1 : prevIndex - 1
    );
  };

  const handleKeyDown = (event: React.KeyboardEvent<HTMLDivElement>) => {
    if (event.key === "ArrowLeft") {
      handleFullscreenPrev();
    } else if (event.key === "ArrowRight") {
      handleFullscreenNext();
    }
  };

  return (
    <>
      <div className="grid grid-cols-3 gap-4">
        {images.map((imageUrl, index) => (
          <motion.div
            key={SERVER_API + "/" + imageUrl.Location}
            className="relative cursor-pointer rounded-md overflow-hidden"
            onClick={() => handleImageClick(index)}
            initial={{ scale: 0.95 }}
            whileHover={{ scale: 1 }}
          >
            <img
              src={SERVER_API + "/" + imageUrl.Location}
              alt={`Game ${index}`}
              className="w-full h-40 object-cover"
            />
          </motion.div>
        ))}
      </div>
      {isFullscreen && (
        <motion.div
          className="fixed inset-0 z-50 flex items-center justify-center"
          onKeyDown={handleKeyDown}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
        >
          <motion.div
            className="absolute inset-0 bg-gray-900 bg-opacity-80"
            onClick={(event) => event.stopPropagation()}
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
          ></motion.div>
          <motion.div
            className="relative max-w-3xl mx-auto my-4"
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: 50 }}
          >
            <img
              src={SERVER_API + "/" + images[selectedImageIndex].Location}
              alt={`Game ${selectedImageIndex}`}
              className="w-full h-full object-contain"
            />
            <motion.button
              className="absolute top-1/2 left-2 transform -translate-y-1/2 text-white"
              onClick={handleFullscreenPrev}
              whileTap={{ scale: 1.5 }}
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
            >
              <FaArrowLeft size={24} />
            </motion.button>
            <motion.button
              className=" absolute top-1/2 right-2 transform -translate-y-1/2 text-white"
              onClick={handleFullscreenNext}
              whileTap={{ scale: 1.5 }}
              initial={{
                opacity: 0,
              }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
            >
              <FaArrowRight size={24} />
            </motion.button>
            <motion.button
              className="absolute top-2 right-2 text-white"
              onClick={handleFullscreenClose}
              whileTap={{ scale: 1.5 }}
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
            >
              <FaTimes size={24} />
            </motion.button>
          </motion.div>
        </motion.div>
      )}
    </>
  );
};

export default GameImages;
