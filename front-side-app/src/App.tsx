import React, { useEffect } from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter as Router, Route, Link, Routes } from "react-router-dom";
import { Content } from "antd/es/layout/layout";
import Navbar from "./components/Navbar";
import Hero from "./components/Hero";
import ShortDescription from "./components/ShortDescription";
import Footer from "./components/Footer";
import Login from "./pages/login";
import MainPage from "./pages/mainPage";
import Register from "./pages/register";
import { Toaster } from "react-hot-toast";
import { CheckJWTAndSession } from "./services/midlewear/AuthVerify";
import { atom, useRecoilState } from "recoil";
import { validTokenAtom } from "./services/constants/globalStates";

function App() {
  const [validToken, setValidToken] = useRecoilState(validTokenAtom);
  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();
      setValidToken(check);
    };
    validateToken();
  }, []);

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
