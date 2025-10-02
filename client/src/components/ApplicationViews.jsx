import { Route, Routes } from "react-router-dom";
import { Home } from "./Home.jsx";
import { Quiz } from "./Quiz/Quiz.jsx";
import { ProtectedRoute } from "./ProtectedRoute.jsx";
import { Profile } from "./Profile.jsx";



export default function ApplicationViews() {
  return (
    <Routes>
      <Route path="/"/>
        <Route
          index
          element={<Home />}/>
       
       <Route path="/quiz" 
       element={
        <ProtectedRoute>
       <Quiz/>
       </ProtectedRoute>
       }/>
       <Route path="/profile" element={<ProtectedRoute><Profile/></ProtectedRoute>}/>
        
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
