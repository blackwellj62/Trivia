export const getCategories = () => {
    return fetch("/api/categories").then((res) => res.json())
}