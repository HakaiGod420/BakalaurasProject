import dayjs from "dayjs";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import NoImageFile from "../../assets/images/noImage.png";
import { SERVER_API } from "../../services/constants/ClienConstants";
import { TableTopGameCard } from "../../services/types/TabletopGame";

const GameCard: React.FC<TableTopGameCard> = ({
  GameBoardId,
  Title,
  ReleaseDate,
  ThumbnailURL,
  ThumbnailName,
}) => {
  const [isHovering, setIsHovering] = useState(false);

  return (
    <Link to={"./" + GameBoardId}>
      <div
        className={`mb-5 relative flex flex-col justify-between rounded-lg overflow-hidden shadow-lg ${
          isHovering
            ? "scale-110 transform transition duration-500 ease-in-out"
            : ""
        }`}
        onMouseEnter={() => setIsHovering(true)}
        onMouseLeave={() => setIsHovering(false)}
      >
        {ThumbnailURL !== null ? (
          <img
            src={SERVER_API + ThumbnailURL}
            alt={ThumbnailName}
            className="w-full h-64 object-cover"
          />
        ) : (
          <img
            src={NoImageFile}
            alt={ThumbnailName}
            className="w-full h-64 object-cover"
          />
        )}

        <div className="bg-gray-900 text-white p-4 max-h-[130px] min-h-[130px]">
          <h3 className="text-lg font-bold">{Title}</h3>
          <p className="text-sm">{dayjs(ReleaseDate).format("YYYY-MM-DD")}</p>
        </div>
      </div>
    </Link>
  );
};

export default GameCard;
