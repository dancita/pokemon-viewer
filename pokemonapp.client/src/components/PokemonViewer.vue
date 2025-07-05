<template>
  <div class="pokemon-component">
    <h1>Pokemon Viewer</h1>
    <div>
      <input id="pokemonQuery" name="pokemonQuery" v-model="inputText" type="text" placeholder="Enter a name or id" />
      <div class="info-icon" tabindex="0" aria-label="More info">
        ℹ️
        <span class="tooltip-text">
          This field only accepts numbers (id) or non-special characters. If the Pokemon you want to search has special characters in their name,
          simply replace it with a hyphen. For example, you can find "Mr.Mime" by searching for "mr-mime".
        </span>
      </div>
    </div>
    <button @click="handleClick">Submit</button>
    <div v-if="errorMessage" class="error-message">
      {{ errorMessage }}
    </div>
    <div v-if="loading" class="loading">
      Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
    </div>

    <div v-if="post" class="content">
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Height</th>
            <th>Weight</th>
            <th>Types</th>
            <th>Abilities</th>
            <th>Image</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="post">
            <td>{{ post.name }}</td>
            <td>{{ post.height }}</td>
            <td>{{ post.weight }}</td>
            <td>
              <div v-for="t in post.types" :key="t.slot" class="type-pill">
                <strong>Slot:</strong> {{ t.slot }} <br />
                <strong>Name:</strong> {{ t.type.name }} <br />
                <a :href="t.type.url" target="_blank" rel="noopener noreferrer">
                  Url
                </a>
                <hr />
              </div>
            </td>
            <td>
              <div v-for="t in post.abilities" :key="t.slot" class="type-pill">
                <strong>IsHidden: </strong> {{ t.is_hidden ? 'Yes' : 'No' }} <br />
                <strong>Slot: </strong> {{ t.slot }} <br />
                <strong>Ability: </strong> {{ t.ability.name }} <br />
                <a :href="t.ability.url" target="_blank" rel="noopener noreferrer">
                  Url
                </a>
              </div>
            </td>
            <td>
              <div>
                <img :src="post?.sprites?.frontDefaultImage" />
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="js">
  import { defineComponent } from 'vue';

  export default defineComponent({
    data() {
      return {
        loading: false,
        post: null,
        inputText: '',
        errorMessage: ''
      };
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
          var response = await fetch(`https://localhost:7222/api/pokemon/${this.inputText}`);

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
        if (!validInput){
          this.errorMessage = "Only numbers and letters allowed."
          return;
        }
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

  th {
    font-weight: bold;
  }

  th, td {
    padding-left: .5rem;
    padding-right: .5rem;
  }

  .pokemon-component {
    text-align: center;
  }

  table {
    margin-left: auto;
    margin-right: auto;
  }

  .type-pill {
    display: inline-block;
    background-color: #e0e0e0;
    color: #333;
  }

  .input-with-info {
    display: flex;
    align-items: center;
    gap: 0.5rem;
    position: relative;
  }

  .info-icon {
    position: relative;
    cursor: default;
    font-size: 1.5rem;
    user-select: none;
    outline: none;
    display: inline;
  }

  .tooltip-text {
    visibility: hidden;
    width: 200px;
    background-color: #333;
    color: #fff;
    text-align: left;
    border-radius: 4px;
    padding: 0.5rem;
    position: absolute;
    bottom: 125%; /* position above the icon */
    left: 50%;
    transform: translateX(-50%);
    opacity: 0;
    transition: opacity 0.3s;
    z-index: 10;
    pointer-events: none;
    font-size: small;
  }

  /* Show tooltip on hover or focus */
  .info-icon:hover .tooltip-text,
  .info-icon:focus .tooltip-text {
    visibility: visible;
    opacity: 1;
    pointer-events: auto;
  }

</style>
