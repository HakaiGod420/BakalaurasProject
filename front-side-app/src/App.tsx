import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter as Router, Route, Link, Routes } from "react-router-dom";
import { Content } from "antd/es/layout/layout";
import Navbar from "./components/Navbar";
import Hero from "./components/Hero";
import ShortDescription from "./components/ShortDescription";
import Footer from "./components/Footer";

function App() {
  return (
    <div>
      <Navbar />
      <Content>
        <Hero />
        <ShortDescription />
        <Routes>
          <Route path="/" />
        </Routes>
      </Content>
      <Footer />
    </div>
  );
}

export default App;
