<script setup>
  import PokemonViewer from './components/PokemonViewer.vue';
  import { useAuth0 } from '@auth0/auth0-vue';
  import LoginButton from './components/LoginButton.vue';
  import LogoutButton from './components/LogoutButton.vue';

  const { loginWithRedirect, logout, isAuthenticated, user, getAccessTokenSilently } = useAuth0();
</script>

<template>
  <div v-if="isAuthenticated == true" class="logout-button-wrapper">
    <LogoutButton />
  </div>
  <header>
    <img alt="Pokemon logo" class="logo" src="./assets/logo.png" />
    <div v-if="!isAuthenticated" class="wrapper">
      <LoginButton />
    </div>
    <div v-else class="wrapper">
      <PokemonViewer />
    </div>
  </header>
</template>

<style scoped>
.app-container {
  position: relative;
}

.app-container > *:first-child {
  position: absolute;
  top: 1rem;
  right: 1rem;
  z-index: 1000; 
}

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
  max-width: 100%;
}

.wrapper {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 1rem;
}

.logout-button-wrapper {
  position: fixed;
  top: 1rem;
  right: 1rem;
  z-index: 999;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
