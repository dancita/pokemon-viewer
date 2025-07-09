<template>
  <div class="pokemon-component">
    <h1>Pokemon Viewer</h1>
    <div class="input-with-tooltip">
      <input id="pokemonQuery" name="pokemonQuery" v-model="inputText" type="text" placeholder="Enter a name or id" />
      <InfoToolTip>
        This field only accepts numbers (id) or non-special characters. If the Pokemon you want to search has special characters in their name,
        simply replace it with a hyphen. For example, you can find "Mr.Mime" by searching for "mr-mime".
      </InfoToolTip>
    </div>
    <button data-testid="submit-button" @click="handleClick">Submit</button>
    <div v-if="errorMessage" class="error-message">
      {{ errorMessage }}
    </div>
    <div v-if="loading" class="loading">
      Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
    </div>
    <PokemonDetailsTable v-if="post" :post="post" />
  </div>
</template>

<script setup>
  import { ref, watch, onMounted } from 'vue';
  import InfoToolTip from './InfoToolTip.vue';
  import PokemonDetailsTable from './PokemonDetailsTable.vue';
  import { useAuth0 } from '@auth0/auth0-vue';
  import { useRoute } from 'vue-router';

  const apiUrl = import.meta.env.VITE_API_BASE_POKEMON_URL;

  const inputText = ref('');
  const errorMessage = ref('');
  const loading = ref(false);
  const post = ref(null);


  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
  const route = useRoute();

  const fetchData = async () => {
    post.value = null;
    loading.value = true;

    try {
      if (!isAuthenticated.value) {
        errorMessage.value = 'User not authenticated'
        return
      }

      const token = await getAccessTokenSilently({
        audience: `${apiUrl}`,
        scope: 'read:pokemon',
      });

      const response = await fetch(`${apiUrl}${inputText.value}`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      if (!response.ok) {
        const { message } = await response.json();

        errorMessage.value = message || "An unknown error occurred.";
        return;
      }

      post.value = await response.json();
    }
    catch (error) {
      errorMessage.value = "A network error occurred. Please try again.";
    }
    finally {
      loading.value = false;
    }
  };

    const handleClick = () => {
      const validInput = /^[A-Za-z0-9]+$/.test(inputText.value);
      const maxLength = 12;
      if (!inputText.value.trim()) {
        errorMessage.value = "Field cannot be empty.";
        return;
      }
      if (!validInput) {
        errorMessage.value = "Only numbers and letters allowed."
        return;
      }
      if (inputText.value.length > maxLength) {
        errorMessage.value = "Maximum 12 characters are allowed."
        return;
      }
      errorMessage.value = "";
      fetchData();
  };
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
    margin-bottom: 30px;
  }

  input {
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    font-size: 16px;
    width: 250px;
    transition: border-color 0.3s ease;
  }

  input:focus {
    border-color: #4CAF50;
    outline: none;
    box-shadow: 0 0 5px rgba(76, 175, 80, 0.3);
  }

  .error-message {
    color: red;
    font-weight: bold;
    margin-top: 10px;
  }

  .pokemon-component {
    text-align: center;
  }

  .input-with-tooltip {
    display: flex;
    align-items: center;
    gap: 0.5rem;
    flex-wrap: nowrap;
    justify-content: center;
    margin-bottom: 1rem;
    position: relative;
  }
</style>
