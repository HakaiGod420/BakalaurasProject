import { motion } from "framer-motion";
import React from "react";
import {
  FaFileAlt,
  FaFileAudio,
  FaFileCode,
  FaFileExcel,
  FaFileImage,
  FaFilePdf,
  FaFileVideo,
  FaFileWord,
} from "react-icons/fa";
import { IoMdDownload } from "react-icons/io";
import { SERVER_API } from "../../services/constants/ClienConstants";
import { TabletopAditionalFile } from "../../services/types/TabletopGame";

interface AditionalFilesProps {
  files: TabletopAditionalFile[];
}

const getFileTypeIcon = (fileName: string) => {
  const extension = fileName.split(".").pop()?.toLowerCase();
  switch (extension) {
    case "pdf":
      return <FaFilePdf size={20} />;
    case "doc":
    case "docx":
      return <FaFileWord size={20} />;
    case "xls":
    case "xlsx":
      return <FaFileExcel size={20} />;
    case "png":
    case "jpg":
    case "jpeg":
    case "gif":
      return <FaFileImage size={20} />;
    case "mp3":
    case "wav":
    case "aac":
      return <FaFileAudio size={20} />;
    case "mp4":
    case "avi":
    case "mov":
      return <FaFileVideo size={20} />;
    case "js":
    case "jsx":
    case "ts":
    case "tsx":
    case "html":
    case "css":
      return <FaFileCode size={20} />;
    default:
      return <FaFileAlt size={20} />;
  }
};

const AditionalFiles: React.FC<AditionalFilesProps> = ({ files }) => {
  return (
    <div className="bg-gray-800 p-6 rounded-lg">
      <h2 className="text-lg font-bold mb-4 text-gray-100">Additional Files</h2>
      <ul className="grid gap-4">
        {files.map((file, index) => (
          <motion.li
            key={index}
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: index * 0.1 }}
            className="flex justify-between items-center bg-gray-900 px-4 py-3 rounded-lg shadow-md"
          >
            <div className="flex items-center">
              {getFileTypeIcon(file.FileName)}
              <span className="text-gray-100 ml-2">{file.FileName}</span>
            </div>
            <a
              href={SERVER_API + "/" + file.Location}
              download
              className="text-gray-400 hover:text-gray-100"
            >
              <IoMdDownload size={20} />
            </a>
          </motion.li>
        ))}
      </ul>
    </div>
  );
};

export default AditionalFiles;
