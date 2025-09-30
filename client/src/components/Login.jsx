import { SignIn } from "@clerk/clerk-react"
import "./Login.css"

export const Login = () => {
    return(
        <div className="login">
            <SignIn/>
        </div>
    )
}