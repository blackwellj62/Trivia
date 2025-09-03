import { useEffect, useState } from "react"
import "./Categories.css"
import { getCategories } from "../../managers/categoryManager.js"

export const Categories = () => {
    const [categories, setCategories] = useState([])

    useEffect(()=>{
        getCategories().then(categoryArray=>{
            setCategories(categoryArray)
        })
    },[])

    return(
        <div>
            {categories.map(category =>
                <button>{category.name}</button>
            )}
        </div>
    )
}