import React, { useState, useEffect } from 'react'
import { ReleaseVersionText, ReleaseVersionLink } from './ReleaseVersionText'
import githubApi from '../../api/github'

const ReleaseVersion = () => {
  return (
    <span>
      {version && (
        <a href={ReleaseVersionLink}>
          <ReleaseVersionText />
        </a>
      )}
    </span>
  )
}

export default ReleaseVersion
