import './assets/main.css';

import { createApp } from 'vue';
import { createAuth0 } from '@auth0/auth0-vue';
import App from './App.vue';
import router from './router';

const app = createApp(App);
const apiUrl = import.meta.env.VITE_API_BASE_POKEMON_URL;

app.use(
  createAuth0({
    domain: "dev-ohcwl04hyco5phi8.us.auth0.com",
    clientId: "QGVHMPjnHPr95Dt9GLGvOn2FZmXo3gf1",
    authorizationParams: {
      redirect_uri: window.location.origin,
      audience: `${apiUrl}`,
      scope: "read:pokemon"
    }
  })
);

app.use(router)
  .mount('#app')
