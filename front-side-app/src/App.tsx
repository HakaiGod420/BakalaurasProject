import React from "react";
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
import Reigster from "./pages/register";

function App() {
  return (
    <div>
      <Navbar />
      <Content>
        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route path="/login/*" element={<Login />} />
          <Route path="/register/*" element={<Reigster />} />
        </Routes>
      </Content>
      <Footer />
    </div>
  );
}

export default App;
