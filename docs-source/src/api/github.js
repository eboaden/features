const githubApiUrl = 'https://api.github.com'

export default {
  getLatestVersion: async () => {
    try {
      const response = await fetch(
        `${githubApiUrl}/repos/eboaden/features/releases/latest`,
        {
          method: 'GET',
        },
      )
      if (response.status === 200) {
        return await response.json()
      }
      throw response.statusText
    } catch (err) {
      throw new Error(err)
    }
  },
}
