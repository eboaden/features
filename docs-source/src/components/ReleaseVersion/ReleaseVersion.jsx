import React, { useState, useEffect } from 'react'
import { Link, Button } from '@material-ui/core'
import Icon from '@mdi/react'
import { mdiTag } from '@mdi/js'
import githubApi from '../../api/github'

const ReleaseVersion = () => {
  const [latestVersion, setLatestVersion] = useState(null)
  useEffect(async () => {
    setLatestVersion(await githubApi.getLatestVersion())
  }, [])
  return latestVersion ? (
    <Link
      href={latestVersion.html_url}
      color="inherit"
      target="_blank"
      style={{
        textDecoration: 'none',
      }}
    >
      <Button color="inherit">
        <Icon path={mdiTag} title="Tag" size={1} />
        &nbsp;
        {latestVersion.name}
      </Button>
    </Link>
  ) : (
    <span />
  )
}

export default ReleaseVersion
