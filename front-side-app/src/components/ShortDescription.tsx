import { Image } from "antd";
import { Link } from "react-router-dom";

export default function ShortDescription() {
  return (
    <div className="w-full bg-white py-16 px-4">
      <div className="max-w-[1240px] mx-auto grid md:grid-cols-2">
        <div className="object-cover w-[380px] mx-auto my-4">
          <Image
            preview={false}
            className="object-cover w-[350px] mx-auto my-4"
            src={require("../assets/images/ImagesGameBoard.jpg")}
            alt=""
          />
        </div>
        <div className="flex flex-col justify-center">
          <p className="text-[#00df9a] font-bold uppercase ">
            YOUR FAVORITE TABLE TOP GAME ITS HERE
          </p>
          <h1 className="md:text-4xl sm:text-3xl text-2xl font-bold py-2 text-black">
            Select your tabletop game and see who wants to play it
          </h1>
          <p className="text-black">
            Tabletop games provide a great opportunity to disconnect from
            screens and engage in face-to-face interactions. With a variety of
            games available, from classics like chess and Monopoly to newer
            options like Settlers of Catan and Ticket to Ride, there is
            something for everyone. So why not take a break from technology and
            enjoy some old-fashioned tabletop gaming with friends and family?
          </p>
          <Link to={"/companies"}>
            <button className="bg-black w-[200px] rounded-md font-medium my-6 mx-auto py-3 text-white md:mx-0 hover:bg-green-600 hover:transition-colors">
              Go To Games
            </button>
          </Link>
        </div>
      </div>
    </div>
  );
}
