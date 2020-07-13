import React from 'react'
import { Typography } from '@material-ui/core'
import Gist from 'react-gist'

const GettingStarted = () => (
  <>
    <Typography variant="h4">Getting Started</Typography>
    <Gist id="59b43cab4242e24d03ae1d53ed158a6f" file="deployment.yaml" />
    <Gist id="59b43cab4242e24d03ae1d53ed158a6f" file="service.yaml" />
  </>
)

export default GettingStarted
