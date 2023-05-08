import React, { useCallback, useEffect, useRef, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { getBoardGameList } from "../services/api/GameBoardService";
import { Filter, TableTopGameCard } from "../services/types/TabletopGame";
import FilterComponent from "./core/FilterComponent";
import GameCard from "./core/GameCard";
import LoadingComponent from "./core/LoadingComponent";

const emptyFilter: Filter = {
  title: "",
  rating: "",
  creationDate: null,
  categories: [],
  types: [],
};
const GameCardList: React.FC = () => {
  const [isLoading, setIsLoading] = useState(false);

  const [totalCount, setTotalCount] = useState(0);

  const [gameBoards, setGameBoards] = useState<TableTopGameCard[] | undefined>(
    []
  );

  const [filters, setFilters] = useState<Filter>(emptyFilter);

  const [searchTerm, setSearchTerm] = useSearchParams();

  const filterData = async (toClear: boolean) => {
    console.log(toClear);
    setIsLoading(true);
    setGameBoards([]);
    if (toClear) {
      setFilters(emptyFilter);
      const response = await getBoardGameList(
        0,
        5,
        searchTermText,
        emptyFilter
      );
      setGameBoards(response?.BoardGames);
      setTotalCount(response?.TotalCount!);
      setIsLoading(false);
      return;
    }
    const response = await getBoardGameList(0, 5, searchTermText, filters);
    setGameBoards(response?.BoardGames);
    setTotalCount(response?.TotalCount!);
    setIsLoading(false);
  };

  const searchTermText = searchTerm.get("searchTerm");

  const observer = useRef<IntersectionObserver | null>(null);

  const loadMore = useCallback(
    (entries: IntersectionObserverEntry[], observer: IntersectionObserver) => {
      const target = entries[0];

      if (target.isIntersecting && totalCount > gameBoards?.length!) {
        setIsLoading(true);
        setTimeout(async () => {
          const startIndex = gameBoards ? gameBoards.length : 0;
          const endIndex = startIndex + 5;
          const newGames = await getBoardGameList(
            startIndex,
            endIndex,
            searchTermText,
            filters
          );
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
      const response = await getBoardGameList(0, 8, searchTermText, filters);
      setGameBoards(response?.BoardGames);
      setTotalCount(response?.TotalCount!);
    };
    fetchGameBoards();
  }, []);

  return (
    <div className="max-w-[1240px] mx-auto mt-10">
      <FilterComponent
        filters={filters}
        setFilters={setFilters}
        submitFilter={filterData}
      />
      <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 gap-4 mt-5">
        {gameBoards?.map((game) => (
          <GameCard key={game.GameBoardId} {...game} />
        ))}
        <div
          id="sentinel"
          className="col-span-full flex justify-center my-4"
          style={{ minHeight: "20px" }}
        >
          {isLoading && <LoadingComponent />}
          {!isLoading && gameBoards?.length === 0 && (
            <div className="text-black text-[25px] p-10">
              No table top games was found
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default GameCardList;
