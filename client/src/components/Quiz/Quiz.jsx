import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom"
import { getQuestions } from "../../managers/questionsManager.js";

export const Quiz = () => {
const [searchParams] = useSearchParams();
const categoryId = searchParams.get("category")
const [questions, setQuestions] = useState([])
// const [currentIndex, setCurrentIndex] = useState({})
// const [score, setScore] = useState("0")
// const [loading, setLoading] = useState("false")
// const [selectedAnswer, setSelectedAnswer] = useState({})

useEffect(()=>{
   getQuestions(categoryId).then(data=>{
    const questObj = data
    setQuestions(questObj)
   }) 
},[categoryId])



    return(
        <h1>Quiz will go here</h1>
    )
}