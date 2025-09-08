import { Route, Routes } from "react-router-dom";
import { Home } from "./Home.jsx";



export default function ApplicationViews() {
  return (
    <Routes>
      <Route path="/"/>
        <Route
          index
          element={<Home />}/>
       
       
        
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
