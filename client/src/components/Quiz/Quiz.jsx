import { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom"
import { getQuestions } from "../../managers/questionsManager.js";
import "./Quiz.css"

export const Quiz = () => {
const [searchParams] = useSearchParams();
const categoryId = searchParams.get("category")
const [questions, setQuestions] = useState([])
const [currentIndex, setCurrentIndex] = useState(0)
const [score, setScore] = useState(0)
const [selectedAnswer, setSelectedAnswer] = useState(null)
const [isAnswered, setIsAnswered] = useState(false)
 const navigate = useNavigate()




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
            <p>Your Score: {score} out of {questions.length}</p>
            <button onClick={()=>{
            setCurrentIndex(0);
            setScore(0)}}>
            Restart</button>
            <button onClick={()=>{navigate("/")}}>Home</button>
        </div>
    )
}

const currentQuestion = questions[currentIndex]


    return (
  <div>
    <h3>Question: {currentIndex + 1}/{questions.length}</h3>
    <h2>{currentQuestion.text}</h2>

    <ul>
      {currentQuestion.allAnswers.map((answer, idx) => {
        let className = "answer-btn";

        if (isAnswered) {
          if (answer === currentQuestion.correctAnswer) {
            className += " correct";
          } else if (answer === selectedAnswer) {
            className += " incorrect";
          }
        } else if (selectedAnswer === answer) {
          className += " selected";
        }

        return (
          <li key={idx}>
            <button
              className={className}
              onClick={() => !isAnswered && setSelectedAnswer(answer)}
              disabled={isAnswered}   // 🔹 disables all buttons once submitted
            >
              {answer}
            </button>
          </li>
        );
      })}
    </ul>

    <button
      onClick={() => {
        if (!isAnswered) {
          if (selectedAnswer === currentQuestion.correctAnswer) {
            setScore(prev => prev + 1);
          }
          setIsAnswered(true); // lock answers + show feedback
        } else {
          setSelectedAnswer(null);
          setIsAnswered(false);
          setCurrentIndex(currentIndex + 1); // move on
        }
      }}
      disabled={!selectedAnswer && !isAnswered}
    >
      {isAnswered ? "Next" : "Submit"}
    </button>
  </div>
);
}
