// import { useEffect, useState } from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Spinner } from "reactstrap";
import ApplicationViews from "./components/ApplicationViews";
import NavBar from "./components/NavBar/NavBar.jsx";

function App() {
  
  return (
    <>
      {/* <NavBar/> */}
      <ApplicationViews/>
    </>
  );
}

export default App;
