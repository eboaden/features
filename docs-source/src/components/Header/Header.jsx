import React from 'react'
import { Link as RouterLink } from 'react-router-dom'
import {
  AppBar,
  Toolbar,
  Typography,
  makeStyles,
  Link,
  Button,
} from '@material-ui/core'
import { GitHub } from '@material-ui/icons'
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
  link: {
    textDecoration: 'none',
    color: 'white',
  },
}))

const Header = () => {
  const classes = useStyles()
  return (
    <AppBar position="fixed" className={classes.appBar}>
      <Toolbar width="100%">
        <div className={classes.appBarContent}>
          <div>
            <RouterLink to="/" className={classes.link}>
              <Typography variant="h6">Features</Typography>
            </RouterLink>
          </div>
          <div>
            <Typography variant="h6">
              <Link
                href="https://github.com/eboaden/features"
                target="_blank"
                color="inherit"
              >
                <Button color="inherit">
                  <GitHub />
                </Button>
              </Link>
              <ReleaseVersion />
            </Typography>
          </div>
        </div>
      </Toolbar>
    </AppBar>
  )
}

export default Header
