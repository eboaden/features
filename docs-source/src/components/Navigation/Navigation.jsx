import React from 'react'
import {
  makeStyles,
  Drawer,
  Toolbar,
  List,
  ListItem,
  Divider,
  ListItemText,
  Link,
} from '@material-ui/core'

const drawerWidth = 240

const useStyles = makeStyles((theme) => ({
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

const Navigation = () => {
  const classes = useStyles()
  return (
    <Drawer
      className={classes.drawer}
      variant="permanent"
      classes={{
        paper: classes.drawerPaper,
      }}
    >
      <Toolbar />
      <div className={classes.drawerContainer}>
        <List>
          <Link
            href="https://github.com/eboaden"
            color="inherit"
            className={classes.link}
          >
            <ListItem button>
              <ListItemText primary="Getting Started" />
            </ListItem>
          </Link>
          <ListItem button>
            <ListItemText primary="Setting up the Demo App" />
          </ListItem>
        </List>
        <Divider />
        <List>
          <ListItem button>
            <ListItemText primary="Nuget Package" />
          </ListItem>
        </List>
      </div>
    </Drawer>
  )
}
export default Navigation
