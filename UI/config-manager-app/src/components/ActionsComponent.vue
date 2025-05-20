<template>
  <div class="mt-4">
    <button class="btn btn-primary" @click="$emit('add')">Add</button>
    <button class="btn btn-primary" @click="$emit('filter')">Filter</button>
    <button class="btn btn-primary" @click="$emit('clearFilter')">
      Clear Filter
    </button>
    <button class="btn btn-primary" @click="$emit('sortByName')">
      Sort by Name
    </button>
    <button class="btn btn-primary" @click="$emit('sortByValue')">
      Sort by Value
    </button>
    <button class="btn btn-primary" @click="$emit('delete')">Delete</button>
    <button class="btn btn-primary" @click="$emit('get')">Get</button>
  </div>
</template>

<script lang="ts">
export default {
  name: "ActionsComponent",
};
</script>
<style scoped>
.btn {
  display: block;
  margin-bottom: 15px;
  width: 200px; /* Set fixed width for consistent button size */
  margin-left: auto; /* Move buttons to the right */
  margin-right: 20px; /* Add some spacing from the right edge */
}

.btn:last-child {
  margin-bottom: 0;
}
</style>
