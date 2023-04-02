import React, { useCallback, useEffect, useRef, useState } from "react";
import { getBoardGameList } from "../services/api/GameBoardService";
import { TableTopGameCard } from "../services/types/TabletopGame";
import GameCard from "./core/GameCard";
import LoadingComponent from "./core/LoadingComponent";

const GameCardList: React.FC = () => {
  const [isLoading, setIsLoading] = useState(false);

  const [totalCount, setTotalCount] = useState(0);

  const [gameBoards, setGameBoards] = useState<TableTopGameCard[] | undefined>(
    []
  );

  const observer = useRef<IntersectionObserver | null>(null);

  const loadMore = useCallback(
    (entries: IntersectionObserverEntry[], observer: IntersectionObserver) => {
      const target = entries[0];

      if (target.isIntersecting && totalCount > gameBoards?.length!) {
        setIsLoading(true);
        setTimeout(async () => {
          const startIndex = gameBoards ? gameBoards.length : 0;
          const endIndex = startIndex + 5;
          const newGames = await getBoardGameList(startIndex, endIndex);
          setGameBoards([...gameBoards!, ...newGames?.BoardGames!]);
          setIsLoading(false);
        }, 1000);
      }
    },
    [gameBoards, totalCount]
  );

  useEffect(() => {
    observer.current = new IntersectionObserver(loadMore, {
      root: null,
      rootMargin: "0px",
      threshold: 1.0,
    });

    if (observer.current && gameBoards ? gameBoards.length > 0 : false) {
      observer.current.observe(document.querySelector("#sentinel")!);
    }

    return () => {
      if (observer.current) {
        observer.current.disconnect();
      }
    };
  }, [gameBoards, loadMore]);

  useEffect(() => {
    const fetchGameBoards = async () => {
      const response = await getBoardGameList(0, 5);
      setGameBoards(response?.BoardGames);
      setTotalCount(response?.TotalCount!);
    };
    fetchGameBoards();
  }, []);

  return (
    <div className="max-w-[1240px] mx-auto mt-10">
      <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4">
        {gameBoards?.map((game) => (
          <GameCard key={game.GameBoardId} {...game} />
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
