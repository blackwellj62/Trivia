import TriviaShmivia from "/src/assets/TriviaShmiviaLogo.png"
import "./Home.css"
import { useEffect, useState } from "react"
import { getCategories } from "../managers/categoryManager.js"
import { useNavigate } from "react-router-dom"

export const Home = () => {
const [categories, setCategories] = useState([])
const [chosenCategory, setChosenCategory] = useState({})
const navigate = useNavigate()

    useEffect(()=>{
        getCategories().then(categoryArray=>{
            setCategories(categoryArray)
        })
    },[])

    return(
        <>
        <div className="home-container">
            <img src={TriviaShmivia} alt="Trivia Shmivia Logo" className="home-logo"/>
        </div>
        <div>
         <select className="category-menu" onChange={(event)=>{setChosenCategory(event.target.value)}}>
            <option value="0">Choose a Category</option>
            {categories.map(category =>
                <option value={category.id} key={category.id}>{category.name}</option>
            )}
         </select>
        </div>
        <div>
            <button onClick={()=>{navigate(`/quiz?category=${chosenCategory}`)}}>Start Quiz</button>
        </div>
        </>
    )
}