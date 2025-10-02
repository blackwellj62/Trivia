import { Link, NavLink as RRNavLink, useNavigate } from "react-router-dom";
import "./NavBar.css";
import { Offcanvas } from "bootstrap"
import TriviaShmivia from "/src/assets/TriviaShmiviaLogo.png"


export default function NavBar() {
  const navigate = useNavigate();

  const closeOffcanvas = () => {
    const offcanvasElement = document.getElementById("offcanvasDarkNavbar");
    const bsOffcanvas = Offcanvas.getInstance(offcanvasElement);
    if (bsOffcanvas) {
      bsOffcanvas.hide();
    }
  };
 

  return (
    <nav className="navbar navbar-dark  fixed-top">
      <div className="container-fluid">
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="offcanvas"
          data-bs-target="#offcanvasDarkNavbar"
          aria-controls="offcanvasDarkNavbar"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div
          className="offcanvas offcanvas-end text-bg-dark"
          tabIndex="-1"
          id="offcanvasDarkNavbar"
          aria-labelledby="offcanvasDarkNavbarLabel"
        >
          <div className="offcanvas-header">
        <RRNavLink className="navbar-brand d-flex align-items-center" to="/">
          <img src={TriviaShmivia} alt="Trivia Shmivia Logo" className="logo me-2" />
          
        </RRNavLink>
            <button
              type="button"
              className="btn-close btn-close-white"
              data-bs-dismiss="offcanvas"
              aria-label="Close"
            ></button>
          </div>
          <div className="offcanvas-body">
            <ul className="navbar-nav justify-content-end flex-grow-1 pe-3">
              <li className="nav-item">
                <RRNavLink to="/" className="nav-link" onClick={closeOffcanvas}>
                Home
                </RRNavLink>
              </li>
              <li className="nav-item">
                <RRNavLink to="profile" className="nav-link" onClick={closeOffcanvas}>
                Profile
                </RRNavLink>
              </li>
             
            </ul>

           
          </div>
        </div>
      </div>
    </nav>
  );
}