import { Image } from "antd";
import { useRecoilState } from "recoil";
import { validTokenAtom } from "../services/constants/recoil/globalStates";
import CreateTabletopGameModal from "./CreateTabletopGameModal";

function SelectOptionGameBoard() {
  const [validToken] = useRecoilState(validTokenAtom);
  return (
    <div className="max-w-[1240px] flext items-center justify-center mx-auto p-10 border-2 shadow-md border-green-500">
      <div className="mx-auto grid lg:grid-cols-2 p-3 mb-5">
        <div className="object-cover w-[380px] mx-auto my-4">
          <Image
            preview={false}
            className="w-[350px] mx-auto my-4 rounded-lg"
            src={require("../assets/images/boardGame.jpg")}
            alt=""
          />
        </div>

        <div className="flex flex-col justify-center">
          <p className="text-[#00df9a] font-bold uppercase ">
            CREATE OR INVITE YOUR FAVORITE TABLE TOP GAME
          </p>
          <h1 className="md:text-4xl sm:text-3xl text-2xl font-bold py-2 text-black">
            Create your table top game
          </h1>
          <p className="text-black">
            Welcome to our website, the place for table board game lovers. Our
            website has two main functions: to share your favorite table board
            games and to invite others to play with you. You can browse through
            hundreds of games, rate them, write reviews and see what others
            think. You can also create or join events where you can meet new
            people and play your favorite games in person or online. Join us
            today and enjoy the fun of table board games!🎲
          </p>
        </div>
      </div>

      {validToken ? (
        <div className="flex flex-col w-full lg:flex-row">
          <div className="grid h-10 flex-grow card rounded-box place-items-center">
            <div className="min-w-full min-h-full">
              <CreateTabletopGameModal />
            </div>
          </div>
          <div className="divider lg:divider-horizontal divide-x-4  font-bold text-black">
            OR
          </div>
          <div className="grid h-10 flex-grow card rounded-box place-items-center">
            <div className="min-w-full min-h-full">
              <CreateTabletopGameModal />
            </div>
          </div>
        </div>
      ) : (
        <div className="text-black flex justify-center font-bold text-[25px]">
          Sign in if you want create table top games
        </div>
      )}
    </div>
  );
}

export default SelectOptionGameBoard;
