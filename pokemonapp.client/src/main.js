import './assets/main.css'

import { createApp } from 'vue'
import { createAuth0 } from '@auth0/auth0-vue'
import App from './App.vue'

const app = createApp(App);

app.use(
  createAuth0({
    domain: "dev-ohcwl04hyco5phi8.us.auth0.com",
    clientId: "QGVHMPjnHPr95Dt9GLGvOn2FZmXo3gf1",
    authorizationParams: {
      redirect_uri: window.location.origin,
      audience: "https://localhost:7222/api/pokemon/",
      scope: "read:pokemon"
    }
  })
);

app.mount('#app')
