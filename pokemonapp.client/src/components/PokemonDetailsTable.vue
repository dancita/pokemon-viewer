<template>
  <div v-if="post" class="content">
    <table>
      <tbody>
        <tr>
          <th>Name</th>
          <td>{{ capitalize(post.name) }}</td>
        </tr>
        <tr>
          <th>Image</th>
          <td>
            <img :src="post?.sprites?.frontDefaultImage" alt="Pokemon image" />
          </td>
        </tr>
        <tr>
          <th>Height</th>
          <td>{{ formatHeight(post.height) }} m</td>
        </tr>
        <tr>
          <th>Weight</th>
          <td>{{ formatWeight(post.weight) }} kg</td>
        </tr>
        <tr>
          <th>Types</th>
          <td>
            <div v-for="t in post.types" :key="t.slot" class="type-pill">
              <strong>Slot:</strong> {{ t.slot }}<br />
              <strong>Name:</strong> {{ t.type.name }}<br />
              <a :href="t.type.url" target="_blank" rel="noopener noreferrer">Url</a>
              <hr />
            </div>
          </td>
        </tr>
        <tr>
          <th>Abilities</th>
          <td>
            <div class="abilities-list">
              <div v-for="t in post.abilities" :key="t.slot" class="type-pill">
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
  defineProps(['post'])

function formatValue(value, multiplier) {
  const result = value * multiplier;
  return Number.isInteger(result) ? result : result.toFixed(1);
}

function formatHeight(decimeters) {
  return formatValue(decimeters, 0.1);
}

function formatWeight(hectograms) {
  return formatValue(hectograms, 0.1);
}

function capitalize(name) {
  if (!name) return '';
  return name.charAt(0).toUpperCase() + name.slice(1);
}
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

  .type-pill {
    display: inline-block;
    background-color: #e0e0e0;
    color: #333;
  }

  .abilities-list {
    display: flex;
    flex-direction: column;
    gap: 1rem;
  }
</style>
