import React from 'react'
import { Switch, Route } from 'react-router-dom'
import { makeStyles, Toolbar } from '@material-ui/core'
import GettingStarted from './components/GettingStarted'
import Header from './components/Header'
import Navigation from './components/Navigation'

const drawerWidth = 240

const useStyles = makeStyles((theme) => ({
  root: {
    display: 'flex',
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerPaper: {
    width: drawerWidth,
  },
  drawerContainer: {
    overflow: 'auto',
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
  },
  link: {
    textDecoration: 'none',
  },
}))

const App = () => {
  const classes = useStyles()
  return (
    <div className={classes.root}>
      <Header />
      <Navigation />
      <div className={classes.content}>
        <Toolbar />
        <Switch>
          <Route path="/" exact component={GettingStarted} />
        </Switch>
      </div>
    </div>
  )
}

export default App
