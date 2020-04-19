import React, { Component } from "react";
import { Link } from 'react-router-dom';

class  Nav extends Component {
  render() {
    return (
        <nav className="navbar navbar-expand-md navbar-fixed-top navbar-dark bg-dark main-nav">
        <div className="container">                    
            <ul className="nav navbar-nav">        
                <li className={'nav-item' } >
                    <Link className="nav-link" to="/">Home</Link>
                </li>                              
                <li className={'nav-item' } >
                    <Link className="nav-link" to="/about">About</Link>
                </li>
                <li className={'nav-item' } >
                    <Link className="nav-link" to="/note">Note</Link>
                </li>        
            </ul>         
        </div>
    </nav>
    );} 
}
export default Nav;