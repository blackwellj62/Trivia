import TriviaShmivia from "/src/assets/TriviaShmiviaLogo.png"
import "./Home.css"

export const Home = () => {
    return(
        <div className="home-container">
            <img src={TriviaShmivia} alt="Trivia Shmivia Logo" className="home-logo"/>
        </div>
    )
}