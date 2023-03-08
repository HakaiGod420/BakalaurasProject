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
  isAdminAtom,
  validTokenAtom,
} from "./services/constants/recoil/globalStates";
import {
  CheckJWTAndSession,
  CheckJWTIsAdmin,
} from "./services/midlewear/AuthVerify";

function App() {
  const [, setValidToken] = useRecoilState(validTokenAtom);
  const [, setIsAdmin] = useRecoilState(isAdminAtom);

  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();
      const adminCheck = await CheckJWTIsAdmin();

      setIsAdmin(adminCheck);
      setValidToken(check);
    };
    validateToken();
  }, [setValidToken, setIsAdmin]);

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
        </Routes>
      </Content>
      <Footer />
    </div>
  );
}

export default App;
