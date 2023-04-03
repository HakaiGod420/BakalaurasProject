import GameBoardCard from "../components/GameBoardComponents/GameBoardCard";
import TableTopTabs from "../components/GameBoardComponents/TableTopTabs";

function Gameboard() {
  return (
    <div className="bg-white">
      <div>
        <GameBoardCard />
        <TableTopTabs />
      </div>
    </div>
  );
}

export default Gameboard;
