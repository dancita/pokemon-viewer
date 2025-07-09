<template>
  <div class="pokemon-component">
    <LogoutButton />
    <h1>Pokemon Viewer</h1>
    <div class="input-with-tooltip">
      <input id="pokemonQuery" name="pokemonQuery" v-model="inputText" type="text" placeholder="Enter a name or id" />
      <InfoToolTip>
        This field only accepts numbers (id) or non-special characters. If the Pokemon you want to search has special characters in their name,
        simply replace it with a hyphen. For example, you can find "Mr.Mime" by searching for "mr-mime".
      </InfoToolTip>
    </div>
    <button @click="handleClick">Submit</button>
    <div v-if="errorMessage" class="error-message">
      {{ errorMessage }}
    </div>
    <div v-if="loading" class="loading">
      Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
    </div>
    <PokemonDetailsTable v-if="post" :post="post" />
  </div>
</template>

<script lang="js">
  import { defineComponent } from 'vue';
  import LogoutButton from './LogoutButton.vue';
  import InfoToolTip from './InfoToolTip.vue';
  import PokemonDetailsTable from './PokemonDetailsTable.vue';
  import { useAuth0 } from '@auth0/auth0-vue';

  const apiUrl = import.meta.env.VITE_API_BASE_POKEMON_URL;

  export default defineComponent({
    components: {
      LogoutButton,
      InfoToolTip,
      PokemonDetailsTable
    },
    data() {
      return {
        loading: false,
        post: null,
        inputText: '',
        errorMessage: ''
      }
    },
    setup(){
      const auth0 = useAuth0()
      return { auth0 }
    },
    watch: {
      // call again the method if the route changes
      '$route': 'fetchData'
    },
    methods: {
      async fetchData() {
        this.post = null;
        this.loading = true;
        
        try {

            if (!this.auth0.isAuthenticated) {
              this.error = 'User not authenticated'
              return
            }

            const token = await this.auth0.getAccessTokenSilently({
              audience:`${apiUrl}`,
              scope: 'read:pokemon',
            })

            const response = await fetch(`${apiUrl}${this.inputText}`, {
              headers: {
                 Authorization: `Bearer ${token}`
              }
            });

          if (!response.ok) {
            const { message } = await response.json();
            
            this.errorMessage = message || "An unknown error occurred.";
            return;
          }

          this.post = await response.json();
        }
        catch (error) {
          this.errorMessage = "A network error occurred. Please try again.";
        }
        finally {
          this.loading = false;
        }
      },
      handleClick() {
        var validInput = /^[A-Za-z0-9]+$/.test(this.inputText);
        const maxLength = 12;
        if (!validInput){
          this.errorMessage = "Only numbers and letters allowed."
          return;
        }
        if (this.inputText.length > maxLength) {{
          this.errorMessage = "Maximum 12 characters are allowed."
          return;
        }}
        this.errorMessage = "";
        this.fetchData();        
      }
    },
  });
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
