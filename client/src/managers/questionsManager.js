export const getQuestions = (categoryId) => {
    return fetch(`api/questions/${categoryId}`,{
        method : "POST",
        headers : {
            "Content-Type" : "application/json"
        }
    }).then((res)=>res.json())
}