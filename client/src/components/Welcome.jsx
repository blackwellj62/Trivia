import TriviaShmivia from "/src/assets/TriviaShmiviaLogo.png"
import "./Home.css"
import { useNavigate } from "react-router-dom"

export const Welcome = () => {
    const navigate = useNavigate()


    return(
        <>
        <div className="home-container">
            <img src={TriviaShmivia} alt="Trivia Shmivia Logo" className="home-logo"/>
        </div>
        <button onClick={()=>{navigate("/home")}}>
            Enter
        </button>
        </>
    )
}