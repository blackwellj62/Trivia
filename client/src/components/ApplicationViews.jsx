import { Route, Routes } from "react-router-dom";
import { Home } from "./Home.jsx";
import { Quiz } from "./Quiz/Quiz.jsx";



export default function ApplicationViews() {
  return (
    <Routes>
      <Route path="/"/>
        <Route
          index
          element={<Home />}/>
       
       <Route path="/quiz" element={<Quiz/>}/>
        
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
