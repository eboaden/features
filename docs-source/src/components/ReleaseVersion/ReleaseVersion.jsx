import React, { useState, useEffect } from 'react'
import githubApi from '../../api/github'

const ReleaseVersion = () => {
  const [latestVersion, setLatestVersion] = useState(null)
  useEffect(async () => {
    setLatestVersion(await githubApi.getLatestVersion())
  },[])
  return (
    <span>
      {latestVersion && (
        latestVersion.name
      )}
    </span>
  )
}

export default ReleaseVersion
