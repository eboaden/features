import React from 'react'
import { AppBar, Toolbar, Typography, makeStyles } from '@material-ui/core'
import ReleaseVersion from '../ReleaseVersion'

const useStyles = makeStyles((theme) => ({
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
  },
  appBarContent: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    width: '100%',
  },
}))

const Header = () => {
  const classes = useStyles()
  return (
    <AppBar position="fixed" className={classes.appBar}>
      <Toolbar width="100%">
        <div className={classes.appBarContent}>
          <div>
            <Typography variant="h6">Features Documentation</Typography>
          </div>
          <div>
            <Typography variant="h6">
              Github &nbsp;
              <ReleaseVersion />
            </Typography>
          </div>
        </div>
      </Toolbar>
    </AppBar>
  )
}

export default Header
