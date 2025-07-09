import './assets/main.css';

import { createApp } from 'vue';
import { createAuth0 } from '@auth0/auth0-vue';
import App from './App.vue';
import router from './router';

const app = createApp(App);
const apiUrl = import.meta.env.VITE_API_BASE_POKEMON_URL;
const accountDomain = import.meta.env.VITE_AUTH0_DOMAIN;
const accountClientId = import.meta.env.VITE_AUTH0_CLIENT_ID;

app.use(
  createAuth0({
    domain: accountDomain,
    clientId: accountClientId,
    authorizationParams: {
      redirect_uri: window.location.origin,
      audience: `${apiUrl}`,
      scope: "read:pokemon"
    }
  })
);

app.use(router)
  .mount('#app')
