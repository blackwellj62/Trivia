import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom"
import { getQuestions } from "../../managers/questionsManager.js";

export const Quiz = () => {
const [searchParams] = useSearchParams();
const categoryId = searchParams.get("category")
const [questions, setQuestions] = useState([])
const [currentIndex, setCurrentIndex] = useState(0)
// const [score, setScore] = useState("0")
// const [loading, setLoading] = useState("false")
// const [selectedAnswer, setSelectedAnswer] = useState({})

useEffect(()=>{
   getQuestions(categoryId).then(data=>{
    const questObj = data
    setQuestions(questObj)
   }) 
},[categoryId])

if (questions.length === 0){
    return <p>Loading Questions...</p>
}

const currentQuestion = questions[currentIndex]


    return(
        <div>
            <h2>{currentQuestion.text}</h2>
            <ul>
                {currentQuestion.answers.map(answer=>(
                    <li key={answer.id}>{answer.text}</li>
                ))}
                <li>{currentQuestion.correctAnswer}</li>
            </ul>
        </div>
    )
}