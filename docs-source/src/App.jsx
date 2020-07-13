import React from 'react'
import { Switch, Route } from 'react-router-dom'
import HomePage from './components/HomePage'
import Header from './components/Header'
import Navigation from './components/Navigation'

function App() {
  return (
    <>
      <Header />
      <Navigation />
      <Switch>
        <Route path="/" exact component={HomePage} />
      </Switch>
    </>
  )
}

export default App
