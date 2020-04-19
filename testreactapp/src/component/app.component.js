import React, { Component } from "react";
import Note from './note.component';
import About from './about.component';
import Nav from './nav.component';

import {BrowserRouter as Router, Switch, Route} from 'react-router-dom';
import { withTranslation } from 'react-i18next';

class App extends Component {
  render() {
    const { t, i18n } = this.props;

    return (
        <Router>       
            <div style={{width:'100%'}} >
                <Nav />
                <Switch>
                    <Route path="/" exact component={Home} />
                    <Route path="/note" render={(props) =>
                         <Note {...props} i18n={i18n} withTranslation={t} />} />
                    <Route path="/about" component={About}/>
                </Switch>  
            </div>                   
        </Router> 
    );}
}

const Home = () => (
    <div className="d-flex justify-content-center">
        <h3>Home Page</h3>
    </div>    
);

export default withTranslation('common')(App);