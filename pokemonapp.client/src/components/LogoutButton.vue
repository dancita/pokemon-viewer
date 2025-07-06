<template>
  <button @click="handleLogout" class="logout-button">
    Logout
  </button>
</template>

<script setup>
import { useAuth0 } from '@auth0/auth0-vue'

const { logout } = useAuth0()

const handleLogout = () => {
  logout({
    logoutParams: {
      returnTo: window.location.origin // redirect after logout
    }
  })
}
</script>

<style scoped>
  .logout-button {
    padding: 8px 16px;
    background-color: #f44336;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  }

    .logout-button:hover {
      background-color: #d32f2f;
    }
</style>
