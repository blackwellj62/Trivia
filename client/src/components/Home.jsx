import TriviaShmivia from "/src/assets/TriviaShmiviaLogo.png"
import "./Home.css"
import { useEffect, useState } from "react"
import { getCategories } from "../managers/categoryManager.js"

export const Home = () => {
const [categories, setCategories] = useState([])

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
         <select className="category-menu">
            <option value="0">Choose a Category</option>
            {categories.map(category =>
                <option value={category.id}>{category.name}</option>
            )}
         </select>
        </div>
        </>
    )
}