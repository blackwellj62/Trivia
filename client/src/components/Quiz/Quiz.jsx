import { useState } from "react";
import { useSearchParams } from "react-router-dom"

export const Quiz = () => {
const [searchParams] = useSearchParams();
const categoryId = searchParams.get("category")
const [questions, setQuestions] = useState([])
const [currentIndex, setCurrentIndex] = useState({})
const [score, setScore] = useState("0")
const [loading, setLoading] = useState("false")
const [selectedAnswer, setSelectedAnswer] = useState({})




    return(
        <h1>Quiz will go here</h1>
    )
}