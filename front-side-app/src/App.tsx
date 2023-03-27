import { Content } from "antd/es/layout/layout";
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

import MyEvents from "./pages/myEvents";
import Settings from "./pages/settings";
import UserProfile from "./pages/userProfile";
import { getActiveInvitationCount } from "./services/api/InvitationService";

function App() {
  const [, setValidToken] = useRecoilState(validTokenAtom);
  const [, setIsAdmin] = useRecoilState(isAdminAtom);
  const [, setActiveInvitations] = useRecoilState(activeInvitations);

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
          <Route path="/profile/:id" element={<UserProfile />} />
          <Route path="/myeventes/" element={<MyEvents />} />
          <Route path="/settings/*" element={<Settings />} />
        </Routes>
      </Content>
      <Footer />
    </div>
  );
}

export default App;
