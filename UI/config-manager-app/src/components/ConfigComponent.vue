<template>
  <div class="container">
    <HeaderComponent />
    <br />
    <div class="row">
      <div class="col-md-9">
        <InputComponent
          :KeyValueInput="KeyValueInput"
          :isLoading="isLoading"
          :KeyValueInputError="KeyValueInputError"
          @addConfig="addConfig"
          @update:KeyValueInput="updateKeyValueInput"
        />
        <br />
        <div class="d-flex justify-content-center" v-if="isLoading">
          <div class="spinner-border" role="status">
            <span class="visually-hidden">Loading...</span>
          </div>
        </div>
        <ListComponent
          :configList="ConfigList"
          :isLoading="isLoading"
          :LoadConfigError="LoadConfigError"
        />
      </div>
      <div class="col-md-3">
        <ActionsComponent
          @add="addConfig"
          @filter="filterConfig"
          @clearFilter="clearFilter"
          @sortByName="sortByName"
          @sortByValue="sortByValue"
          @delete="deleteConfig"
          @get="getConfigs"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from "vue";
import ActionsComponent from "./ActionsComponent.vue";
import InputComponent from "./InputComponent.vue";
import ListComponent from "./ListComponent.vue";
import KeyValuePair from "../Models/KeyValuePair";
import ConfigClient from "../Provider/ConfigClient";
import ResponseError from "@/Models/ResponseError";
import HeaderComponent from "./HeaderCompnent.vue";

const KeyValueInput = ref("");
const KeyValueInputError = ref("");
const ConfigList = ref<KeyValuePair[]>([]);
const LoadConfigError = ref("");
let configClient: ConfigClient;
const isLoading = ref(false);

onMounted(async () => {
  try {
    configClient = new ConfigClient();
    await getConfigs();
  } catch (error) {
    console.error("Error in onMounted:", error);
    if (ResponseError.IsResponseError(error)) {
      LoadConfigError.value = `${error.title}`;
    } else if (typeof error === "string") {
      LoadConfigError.value = error;
    } else {
      LoadConfigError.value = "An unknown error occurred while loading config";
    }
  }
});

const addConfig = async () => {
  KeyValueInputError.value = "";
  LoadConfigError.value = "";
  isLoading.value = true;

  try {
    if (KeyValueInput.value === "") {
      KeyValueInputError.value = "The key/value pair input is required";
      return;
    }

    await configClient.addConfig(KeyValueInput.value);
    //refresh the list with the new config
    await getConfigs();
    KeyValueInput.value = "";
  } catch (error: unknown) {
    if (ResponseError.IsResponseError(error)) {
      KeyValueInputError.value = `${error.title}`;
    } else if (typeof error === "string") {
      KeyValueInputError.value = error;
    } else {
      KeyValueInputError.value =
        "An unknown error occurred while adding config";
    }
    console.error("Error in addConfig:", error);
  } finally {
    isLoading.value = false;
  }
};

const getConfigs = async () => {
  KeyValueInputError.value = "";
  LoadConfigError.value = "";
  isLoading.value = true;
  try {
    const configs = await configClient.getAllConfigs();
    ConfigList.value = configs;
  } catch (error) {
    if (ResponseError.IsResponseError(error)) {
      LoadConfigError.value = `${error.title}`;
    } else if (typeof error === "string") {
      LoadConfigError.value = error;
    } else {
      LoadConfigError.value = "An unknown error occurred while loading configs";
    }
    console.error("Error in getConfigs:", error);
  } finally {
    isLoading.value = false;
  }
};

const filterConfig = async () => {
  KeyValueInputError.value = "";
  LoadConfigError.value = "";
  isLoading.value = true;

  try {
    if (KeyValueInput.value === "") {
      KeyValueInputError.value = "The filter input is required";
      return;
    }
    const filteredConfigs = await configClient.filterConfig(
      KeyValueInput.value
    );
    ConfigList.value = filteredConfigs;
  } catch (error) {
    if (ResponseError.IsResponseError(error)) {
      KeyValueInputError.value = `${error.title}`;
    } else if (typeof error === "string") {
      KeyValueInputError.value = error;
    } else {
      KeyValueInputError.value =
        "An unknown error occurred while filtering configs";
    }
    console.error("Error in filter config:", error);
  } finally {
    isLoading.value = false;
  }
};

const clearFilter = async () => {
  KeyValueInput.value = "";
  await getConfigs();
};

const sortByName = async () => {
  sortConfig(0);
};

const sortByValue = () => {
  sortConfig(1);
};

const sortConfig = async (type: number) => {
  KeyValueInputError.value = "";
  LoadConfigError.value = "";
  isLoading.value = true;

  try {
    const sortedConfigs = await configClient.sortConfig(type);
    ConfigList.value = sortedConfigs;
  } catch (error) {
    if (ResponseError.IsResponseError(error)) {
      KeyValueInputError.value = `${error.title}`;
    } else if (typeof error === "string") {
      KeyValueInputError.value = error;
    } else {
      KeyValueInputError.value =
        "An unknown error occurred while sorting configs";
    }
    console.error("Error in sorting config:", error);
  } finally {
    isLoading.value = false;
  }
};

const deleteConfig = async () => {
  KeyValueInputError.value = "";
  LoadConfigError.value = "";
  isLoading.value = true;

  try {
    await configClient.deleteConfig();
    await getConfigs();
  } catch (error) {
    if (ResponseError.IsResponseError(error)) {
      KeyValueInputError.value = `${error.title}`;
    } else if (typeof error === "string") {
      KeyValueInputError.value = error;
    } else {
      KeyValueInputError.value =
        "An unknown error occurred while deleting configs";
    }
    console.error("Error in delete config:", error);
  } finally {
    isLoading.value = false;
  }
};

const updateKeyValueInput = async (value: string) => {
  KeyValueInput.value = value;
  KeyValueInputError.value = "";
  LoadConfigError.value = "";

  if (KeyValueInput.value.length === 0) {
    await getConfigs();
  }
};
</script>
