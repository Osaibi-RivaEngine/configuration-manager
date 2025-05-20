<template>
  <div class="d-flex justify-content-between">
    <label for="key-value-list"
      >Key/Value pair list ({{ localConfigList.length }}):
    </label>
    <div class="form-check form-switch" v-if="localConfigList.length > 0">
      <input
        class="form-check-input"
        type="checkbox"
        role="switch"
        @change="showTableVersion = !showTableVersion"
        id="swichToTableView"
      />
      <label class="form-check-label" for="swichToTableView"
        >Switch to table view</label
      >
    </div>
  </div>

  <table
    class="table table-striped mt-2"
    id="key-value-list"
    v-if="localConfigList.length > 0 && error === '' && showTableVersion"
  >
    <thead>
      <tr>
        <!-- <th scope="col">#</th> -->
        <th scope="col">Key</th>
        <th scope="col">Value</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(config, index) in localConfigList" :key="index">
        <td>{{ config.Key }}</td>
        <td>{{ config.Value }}</td>
      </tr>
    </tbody>
  </table>

  <div v-if="localConfigList.length === 0" class="pt-4 text-muted">
    <p>
      <i class="fa-regular fa-rectangle-list"></i>
      <small class="ms-2">Configuration list is empty</small>
    </p>
  </div>

  <ul class="text-danger" v-if="error && !isLoading">
    <li>
      <small>{{ error }}</small>
    </li>
  </ul>
  <div v-if="localConfigList.length > 0 && !showTableVersion">
    <div v-for="(config, index) in localConfigList" :key="index" class="mt-1">
      <ul class="list-group">
        <li class="list-group-item">
          {{ config.Label }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { defineProps, ref, watch } from "vue";
import KeyValuePair from "../Models/KeyValuePair";

const props = defineProps<{
  configList: KeyValuePair[];
  LoadConfigError: string;
  isLoading: boolean;
}>();

const localConfigList = ref([...props.configList]);
const error = ref(props.LoadConfigError);
const isLoading = ref(props.isLoading);

const showTableVersion = ref(false);

// Watch for prop changes from parent
watch(
  () => props.configList,
  (newValue) => {
    localConfigList.value = [...newValue];
  },
  { deep: true }
);

watch(
  () => props.LoadConfigError,
  (newValue) => {
    error.value = newValue;
  }
);

watch(
  () => props.isLoading,
  (newValue) => {
    isLoading.value = newValue;
  }
);
</script>
