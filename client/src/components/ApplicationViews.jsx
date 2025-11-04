import { Route, Routes } from "react-router-dom";
import { Home } from "./Home.jsx";
import { Quiz } from "./Quiz/Quiz.jsx";
import { Profile } from "./Profile.jsx";
import { SignedIn, SignedOut, RedirectToSignIn } from "@clerk/clerk-react";



export default function ApplicationViews() {
  return (
    <Routes>
      <Route path="/"/>
        <Route
          index
          element={<Home />}/>
       
       <Route path="/quiz" 
       element={
        <>
        <SignedIn>
       <Quiz/>
       </SignedIn>
       <SignedOut>
        <RedirectToSignIn/>
       </SignedOut>
       </>
       }/>
       <Route path="/profile" element={
          <>
            <SignedIn>
              <Profile />
            </SignedIn>
            <SignedOut>
              <RedirectToSignIn />
            </SignedOut>
          </>
        }/>
        
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
