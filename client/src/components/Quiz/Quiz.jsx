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

const shuffleArray = (array)=>{
    const copy = {...array}
    for (let i = copy.length - 1; i > 0; i --){
        const j = Math.floor(Math.random * (i + 1))
        ;[copy[1],copy[j]] = [copy[j],copy[i]]
    }
    return copy
}


useEffect(() => {
  getQuestions(categoryId).then(data => {
    const withShuffled = data.map(q => {
      const allAnswers = [...q.answers.map(a => a.text), q.correctAnswer];
      const shuffled = allAnswers.sort(() => Math.random() - 0.5);
      return { ...q, allAnswers: shuffled };
    });

    setQuestions(withShuffled);
  });
}, [categoryId])

if (questions.length === 0){
    return <p>Loading Questions...</p>
}

if (currentIndex >= questions.length){
    return(
        <div>
            <h2>Quiz Complete!</h2>
            <button onClick={()=>setCurrentIndex(0)}>Restart</button>
        </div>
    )
}

const currentQuestion = questions[currentIndex]


    return(
        <div>
            <h2>{currentQuestion.text}</h2>
            <ul>
                {currentQuestion.allAnswers.map((answer, idx) => (
        <li key={idx}>{answer}</li>
      ))}
            </ul>
            <button onClick={()=>setCurrentIndex(currentIndex + 1)}
                disabled={currentIndex >= questions.length - 1}>
                Next
            </button>
        </div>
    )
}
