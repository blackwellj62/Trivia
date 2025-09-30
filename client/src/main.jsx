import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter } from 'react-router-dom'
import { ClerkProvider } from '@clerk/clerk-react'

const PUBLISHABLE_KEY = "pk_test_c3VwcmVtZS1zYXdmaXNoLTcwLmNsZXJrLmFjY291bnRzLmRldiQ";

createRoot(document.getElementById('root')).render(
  <ClerkProvider publishableKey={PUBLISHABLE_KEY}>
  <BrowserRouter>
    <App />
  </BrowserRouter>
  </ClerkProvider>
)
