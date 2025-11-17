import { useUser } from "@clerk/clerk-react"
import { useState, useEffect } from "react"



export const Profile = () => {
    const {user, setUser} = useUser()

   
    return(
        <div className="profile-container">
            <h1>Profile</h1>
            <h2>User Information:</h2>
            <p>Name: {user.fullName || "No Name Set"}</p>
            <p>Email: {user.primaryEmailAddress?.emailAddress}</p>
        </div>
    )
}