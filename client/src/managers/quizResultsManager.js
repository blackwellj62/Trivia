export const saveQuizResult = async (quizData) => {
  const res = await fetch("/api/userquizresults", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(quizData),
  });
  return await res.json();
};