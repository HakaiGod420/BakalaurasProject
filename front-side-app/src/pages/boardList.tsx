import GameCardList from "../components/TableTopGameList";

interface GameCardProps {
  title: string;
  releaseDate: string;
  imageUrl: string;
}

function BoardList() {
  return (
    <div className="bg-white">
      <div className="p-2">
        <GameCardList />
      </div>
    </div>
  );
}
export default BoardList;
