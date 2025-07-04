<template>
    <div class="pokemon-component">
        <h1>Pokemon Viewer</h1>

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
                  </tr>
                </thead>
                <tbody>
                  <tr v-if="post">
                    <td>{{ post.name }}</td>
                    <td>{{ post.height }}</td>
                    <td>{{ post.weight }}</td>
                    <td>
                      <div v-for="t in post.types" :key="t.slot" class="type-pill">
                        <strong>Slot:</strong> {{ t.slot }} <br/>
                        <strong>Name:</strong> {{ t.type.name }} <br/>
                        <a :href="t.type.url" target="_blank" rel="noopener noreferrer">
                          Url
                        </a>
                        <hr />
                      </div>
                      </td>
                      <td>
                      <div v-for="t in post.abilities" :key="t.slot" class="type-pill">
                        <strong>IsHidden: </strong> {{ t.is_hidden ? 'Yes' : 'No' }} <br/>
                        <strong>Slot: </strong> {{ t.slot }} <br />
                        <strong>Ability: </strong> {{ t.ability.name }} <br/>
                        <a :href="t.ability.url" target="_blank" rel="noopener noreferrer">
                          Url
                        </a>
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
                post: null
            };
        },
        async created() {
            await this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            async fetchData() {
                this.post = null;
                this.loading = true;

            var response = await fetch('https://localhost:7222/api/pokemon/pikachu');
            if (response.ok) {
             
              this.post = await response.json();
              console.log("post", this.post);
                    this.loading = false;
                }
            }
        },
    });
</script>

<style scoped>
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
</style>
