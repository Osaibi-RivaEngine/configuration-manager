<template>
  <div class="mb-3">
    <label for="key-value-input" class="form-label"
      >Key/Value pair Input:</label
    >
    <input
      class="form-control"
      @keyup="addConfig"
      required
      id="key-value-input"
      placeholder="Enter key value pair or a filter"
      v-model="localValue"
      @input="updateValue"
    />
    <ul class="text-danger" v-if="error">
      <li>
        <small>{{ error }}</small>
      </li>
    </ul>
  </div>
</template>

<script lang="ts" setup>
import { defineProps, defineEmits, ref, watch } from "vue";

const props = defineProps<{
  KeyValueInput: string;
  KeyValueInputError: string;
}>();

const emit = defineEmits<{
  (e: "update:KeyValueInput", value: string): void;
  (e: "addConfig"): void;
}>();

const localValue = ref(props.KeyValueInput);
const error = ref(props.KeyValueInputError);

// Watch for prop changes from parent
watch(
  () => props.KeyValueInput,
  (newValue) => {
    localValue.value = newValue;
  }
);

watch(
  () => props.KeyValueInputError,
  (newValue) => {
    error.value = newValue;
  }
);

// Emit changes to parent
const updateValue = () => {
  emit("update:KeyValueInput", localValue.value);
};

const addConfig = (e: KeyboardEvent) => {
  if (e.key === "Enter") {
    emit("addConfig");
  }
};
</script>
