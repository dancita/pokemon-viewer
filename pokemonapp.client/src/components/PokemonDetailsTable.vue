<template>
  <div v-if="pokemon" class="content">
    <table>
      <tbody>
        <tr>
          <th>Name</th>
          <td>{{ capitalize(pokemon.name) }}</td>
        </tr>
        <tr>
          <th>Image</th>
          <td>
            <img :src="pokemon?.sprites?.frontDefaultImage" alt="Pokemon image" />
          </td>
        </tr>
        <tr>
          <th>Height</th>
          <td>{{ formatHeight(pokemon.height) }} m</td>
        </tr>
        <tr>
          <th>Weight</th>
          <td>{{ formatWeight(pokemon.weight) }} kg</td>
        </tr>
        <tr>
          <th>Types</th>
          <td>
            <div class="properties-list">
              <div v-for="t in pokemon.types" :key="t.slot" class="property-container">
                <strong>Slot:</strong> {{ t.slot }}<br />
                <strong>Name:</strong> {{ t.type.name }}<br />
                <a :href="t.type.url" target="_blank" rel="noopener noreferrer">Url</a>
                <hr />
              </div>
              </div>
          </td>
        </tr>
        <tr>
          <th>Abilities</th>
          <td>
            <div class="properties-list">
              <div v-for="t in pokemon.abilities" :key="t.slot" class="property-container">
                <strong>IsHidden:</strong> {{ t.is_hidden ? 'Yes' : 'No' }}<br />
                <strong>Slot:</strong> {{ t.slot }}<br />
                <strong>Ability:</strong> {{ t.ability.name }}<br />
                <a :href="t.ability.url" target="_blank" rel="noopener noreferrer">Url</a>
                <hr />
              </div>
              </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { capitalize, formatHeight, formatWeight } from '@/utils/pokemonFormatters';

defineProps(['pokemon'])
</script>

<style scoped>
th {
  font-weight: bold;
}

th, td {
  padding-left: .5rem;
  padding-right: .5rem;
}

table {
  margin-left: auto;
  margin-right: auto;
}

.property-container {
  display: inline-block;
  background-color: #e0e0e0;
  color: #333;
}

.properties-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
</style>
