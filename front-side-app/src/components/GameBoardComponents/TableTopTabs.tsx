import { motion } from "framer-motion";
import { useState } from "react";
import { SingleTabletopGame } from "../../services/types/TabletopGame";
import AditionalFiles from "./AditionalFiles";
import GameDescription from "./GameDescription";
import GameImages from "./GameImages";
import GameReviews from "./GameReviews";

type Tab = "description" | "reviews" | "images" | "additional_files";

interface Props {
  gameBoard: SingleTabletopGame;
}

const TableTopTabs = ({ gameBoard }: Props) => {
  const [activeTab, setActiveTab] = useState<Tab>("description");

  const handleTabClick = (tab: Tab) => {
    setActiveTab(tab);
  };

  const tabVariants = {
    hidden: {
      opacity: 0,
      y: 10,
    },
    visible: {
      opacity: 1,
      y: 0,
      transition: {
        duration: 0.3,
      },
    },
  };
  
  return (
    <div className="bg-white p-5">
      <div className="bg-white mx-auto max-w-[1280px] rounded-lg ">
        <div className="flex justify-center rounded-md">
          <motion.div
            className={`${
              activeTab === "description" ? "bg-gray-800" : "bg-gray-700"
            } px-4 py-2 cursor-pointer rounded-tl-lg`}
            onClick={() => handleTabClick("description")}
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.95 }}
            transition={{ duration: 0.1 }}
          >
            Description
          </motion.div>
          <motion.div
            className={`${
              activeTab === "reviews" ? "bg-gray-800" : "bg-gray-700"
            } px-4 py-2 cursor-pointer`}
            onClick={() => handleTabClick("reviews")}
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.95 }}
            transition={{ duration: 0.1 }}
          >
            Reviews
          </motion.div>
          <motion.div
            className={`${
              activeTab === "images" ? "bg-gray-800" : "bg-gray-700"
            } px-4 py-2 cursor-pointer`}
            onClick={() => handleTabClick("images")}
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.95 }}
            transition={{ duration: 0.1 }}
          >
            Images
          </motion.div>
          <motion.div
            className={`${
              activeTab === "additional_files" ? "bg-gray-800" : "bg-gray-700"
            } px-4 py-2 cursor-pointer rounded-tr-lg`}
            onClick={() => handleTabClick("additional_files")}
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.95 }}
            transition={{ duration: 0.1 }}
          >
            Additional Files
          </motion.div>
        </div>
        <div className="bg-gray-800 p-4 rounded-md pb-10">
          {activeTab === "description" && (
            <motion.div
              variants={tabVariants}
              initial="hidden"
              animate="visible"
            >
              <GameDescription
                categories={gameBoard.Categories}
                types={gameBoard.Types}
                description={gameBoard.Description}
                rules={gameBoard.Rules}
              />
            </motion.div>
          )}
          {activeTab === "reviews" && (
            <motion.div
              variants={tabVariants}
              initial="hidden"
              animate="visible"
            >
              <GameReviews />
            </motion.div>
          )}
          {activeTab === "images" && (
            <motion.div
              variants={tabVariants}
              initial="hidden"
              animate="visible"
            >
              <GameImages images={gameBoard.Images} />
            </motion.div>
          )}
          {activeTab === "additional_files" && (
            <motion.div
              variants={tabVariants}
              initial="hidden"
              animate="visible"
            >
              <AditionalFiles files={gameBoard.Files} />
            </motion.div>
          )}
        </div>
      </div>
    </div>
  );
};

export default TableTopTabs;
