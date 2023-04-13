import { Content } from "antd/es/layout/layout";
import jwt_decode from "jwt-decode";
import { useEffect } from "react";
import { Toaster } from "react-hot-toast";
import { Route, Routes } from "react-router-dom";
import { useRecoilState } from "recoil";
import "./App.css";
import Footer from "./components/Footer";
import Navbar from "./components/Navbar";
import Login from "./pages/login";
import MainPage from "./pages/mainPage";
import Register from "./pages/register";
import TableBoadGames from "./pages/tableBoadGames";
import {
  activeInvitations,
  isAdminAtom,
  userName,
  validTokenAtom,
} from "./services/constants/recoil/globalStates";
import {
  CheckJWTAndSession,
  CheckJWTIsAdmin,
} from "./services/midlewear/AuthVerify";

//theme
import "primereact/resources/themes/viva-dark/theme.css";

//core
import "primereact/resources/primereact.min.css";

import "primeicons/primeicons.css";

import AdminMenu from "./pages/AdminPages/adminMenu";
import BoardGamesForReview from "./pages/AdminPages/boardGamesForReview";
import BoardList from "./pages/boardList";
import Gameboard from "./pages/gameboard";
import MyEvents from "./pages/myEvents";
import Settings from "./pages/settings";
import UserProfile from "./pages/userProfile";
import { getActiveInvitationCount } from "./services/api/InvitationService";
import { JWTDeCode } from "./services/types/Miscellaneous";

function App() {
  const [, setUsername] = useRecoilState(userName);
  const [, setValidToken] = useRecoilState(validTokenAtom);
  const [, setIsAdmin] = useRecoilState(isAdminAtom);
  const [, setActiveInvitations] = useRecoilState(activeInvitations);

  const setUsernameInRecoil = () => {
    const token = JSON.parse(localStorage.getItem("token") || "false");

    if (token === false) {
      return false;
    }

    const decoded: JWTDeCode = jwt_decode(token.token);
    setUsername(decoded.Username);
  };
  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();
      const adminCheck = await CheckJWTIsAdmin();

      setIsAdmin(adminCheck);
      setValidToken(check);
    };

    const getActiveInvitations = async () => {
      const response = await getActiveInvitationCount();
      setActiveInvitations(response);
    };
    setUsernameInRecoil();
    validateToken();
    getActiveInvitations();
  }, [setValidToken, setIsAdmin, setActiveInvitations]);

  return (
    <div>
      <Navbar />
      <Content>
        <Toaster />
        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route path="/login/*" element={<Login />} />
          <Route path="/tableboardgames/" element={<TableBoadGames />} />
          <Route path="/register/*" element={<Register />} />
          <Route path="/profile/:username" element={<UserProfile />} />
          <Route path="/myeventes/" element={<MyEvents />} />
          <Route path="/settings/*" element={<Settings />} />
          <Route path="/gameboards/" element={<BoardList />} />
          <Route path="/gameboards/:id" element={<Gameboard />} />
          <Route path="/adminmenu" element={<AdminMenu />} />
          <Route
            path="/adminmenu/review-gameboard"
            element={<BoardGamesForReview />}
          />
        </Routes>
      </Content>
      <Footer />
    </div>
  );
}

export default App;
