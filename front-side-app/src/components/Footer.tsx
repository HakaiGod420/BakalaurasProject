import {
  FaDribbbleSquare,
  FaFacebookSquare,
  FaGithubSquare,
  FaInstagram,
  FaTwitterSquare,
} from "react-icons/fa";
import { Link } from "react-router-dom";

export default function Footer() {
  return (
    <div className="max-w-[1240px] mx-auto py-16 px-4 grid lg:grid-cols-3 gap-8 text-gray-300">
      <div>
        <h1 className="w-full text-3xl font-bold text-[#00df9a]">
          TABLETOPGAMES.
        </h1>
        <p className="py-4">
          TabletopGames is your go-to destination for everything related to
          tabletop gaming. From classic games to the latest releases, we have it
          all. Browse our extensive collection, connect with other players, and
          enjoy the social atmosphere of tabletop gaming. Join us today and take
          your gaming experience to the next level!
        </p>
        <div className="flex justify-between md:w-[75%] my-6">
          <FaFacebookSquare size={30} />
          <FaInstagram size={30} />
          <FaTwitterSquare size={30} />
          <Link to={"https://github.com/HakaiGod420"}>
            <FaGithubSquare size={30} />
          </Link>
          <FaDribbbleSquare size={30} />
        </div>
      </div>
    </div>
  );
}
