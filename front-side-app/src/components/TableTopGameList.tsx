import React, { useCallback, useEffect, useRef, useState } from "react";
import { Link } from "react-router-dom";
import { getBoardGameList } from "../services/api/GameBoardService";
import { TableTopGameCard } from "../services/types/TabletopGame";
import LoadingComponent from "./core/LoadingComponent";

interface GameCardProps {
  title: string;
  releaseDate: string;
  imageUrl: string;
}

const GameCard: React.FC<GameCardProps> = ({
  title,
  releaseDate,
  imageUrl,
}) => {
  const [isHovering, setIsHovering] = useState(false);

  return (
    <Link to={"#"}>
      <div
        className={`mb-5 relative flex flex-col justify-between rounded-lg overflow-hidden shadow-lg ${
          isHovering
            ? "scale-110 transform transition duration-500 ease-in-out"
            : ""
        }`}
        onMouseEnter={() => setIsHovering(true)}
        onMouseLeave={() => setIsHovering(false)}
      >
        <img src={imageUrl} alt={title} className="w-full h-64 object-cover" />
        <div className="bg-gray-900 text-white p-4">
          <h3 className="text-lg font-bold">{title}</h3>
          <p className="text-sm">{releaseDate}</p>
        </div>
      </div>
    </Link>
  );
};

interface GameCardListProps {
  games: GameCardProps[];
  itemsPerLoad: number;
}

const GameCardList: React.FC<GameCardListProps> = ({ games, itemsPerLoad }) => {
  const [loadedGames, setLoadedGames] = useState<GameCardProps[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const [gameBoards, setGameBoards] = useState<TableTopGameCard[] | undefined>(
    []
  );

  const observer = useRef<IntersectionObserver | null>(null);

  const loadMore = useCallback(
    (entries: IntersectionObserverEntry[], observer: IntersectionObserver) => {
      const target = entries[0];

      if (target.isIntersecting) {
        setIsLoading(true);
        setTimeout(() => {
          const startIndex = loadedGames.length;
          const endIndex = startIndex + itemsPerLoad;
          const newGames = games.slice(startIndex, endIndex);
          setLoadedGames([...loadedGames, ...newGames]);
          setIsLoading(false);
        }, 1000);
      }
    },
    [games, itemsPerLoad, loadedGames]
  );

  useEffect(() => {
    observer.current = new IntersectionObserver(loadMore, {
      root: null,
      rootMargin: "0px",
      threshold: 1.0,
    });

    if (observer.current && games.length > 0) {
      observer.current.observe(document.querySelector("#sentinel")!);
    }

    return () => {
      if (observer.current) {
        observer.current.disconnect();
      }
    };
  }, [games, loadMore]);

  useEffect(() => {
    const fetchGameBoards = async () => {
      const response = await getBoardGameList(0, 1);
      setGameBoards(response);
    };
    fetchGameBoards();
  }, []);

  return (
    <div className="max-w-[1240px] mx-auto mt-10">
      {gameBoards?.map((game) => (
        <p>a</p>
      ))}
      <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4">
        {loadedGames.map((game) => (
          <GameCard key={game.title} {...game} />
        ))}
        <div
          id="sentinel"
          className="col-span-full flex justify-center my-4"
          style={{ minHeight: "20px" }}
        >
          {isLoading && <LoadingComponent />}
        </div>
      </div>
    </div>
  );
};

export default GameCardList;
