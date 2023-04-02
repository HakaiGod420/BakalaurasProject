import GameCardList from "../components/TableTopGameList";

interface GameCardProps {
  title: string;
  releaseDate: string;
  imageUrl: string;
}

const games: GameCardProps[] = [
  {
    title: "Dungeons & Dragons",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Monopoly",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
  {
    title: "Risk",
    releaseDate: "2020-01-01",
    imageUrl: "https://i.ytimg.com/vi/vpM5lzkXVHA/maxresdefault.jpg",
  },
];
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
