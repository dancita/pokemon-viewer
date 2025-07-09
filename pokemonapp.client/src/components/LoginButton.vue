<template>
  <button @click="login">
    Login
  </button>
</template>

<script setup>
  import { useAuth0 } from '@auth0/auth0-vue'

  const { loginWithRedirect } = useAuth0()

  const login = () => {
    loginWithRedirect({
      connection: 'google-oauth2'
    })
  }
</script>

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
</style>
