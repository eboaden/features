import React, { useState, useEffect } from 'react'
import githubApi from '../../../api/github'

const [version, setVersion] = useState(null)

const ReleaseVersionText = () => {
  useEffect(() => {
    const fetchVersion = async () => {
      setVersion(await githubApi.getLatestVersion())
    }
    fetchVersion()
  }, [])
  return <span>{version.name}</span>
}

export const ReleaseVersionLink = version.html_url

export { ReleaseVersionText }
