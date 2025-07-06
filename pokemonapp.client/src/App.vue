<script setup>
  import PokemonViewer from './components/PokemonViewer.vue';
  import { useAuth0 } from '@auth0/auth0-vue';
  import LoginButton from './components/LoginButton.vue';

  const { loginWithRedirect, logout, isAuthenticated, user, getAccessTokenSilently } = useAuth0();
</script>

<template>
  <header>
    <img alt="Pokemon logo" class="logo" src="./assets/logo.png" width="500" height="300" />
    <div v-if="!isAuthenticated">
      <LoginButton/>
    </div>
    <div v-else class="wrapper">
      <PokemonViewer />
    </div>
  </header>
</template>

<style scoped>
button {
  background-color: #4CAF50;
  color: white;
  padding: 10px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-weight: bold;
  transition: background-color 0.3s ease;
}

header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
