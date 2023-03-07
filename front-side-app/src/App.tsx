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
import { validTokenAtom } from "./services/constants/globalStates";
import { CheckJWTAndSession } from "./services/midlewear/AuthVerify";

function App() {
  const [, setValidToken] = useRecoilState(validTokenAtom);
  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();
      setValidToken(check);
    };
    validateToken();
  }, [setValidToken]);

  return (
    <div>
      <Navbar />
      <Content>
        <Toaster />
        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route path="/login/*" element={<Login />} />
          <Route path="/register/*" element={<Register />} />
        </Routes>
      </Content>
      <Footer />
    </div>
  );
}

export default App;
