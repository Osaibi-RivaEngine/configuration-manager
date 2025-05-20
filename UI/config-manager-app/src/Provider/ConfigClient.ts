import KeyValuePair from "@/Models/KeyValuePair";
import ConfigDataProvider from "./ConfigDataProvider";
import { AxiosError } from "axios";

class ConfigClient {
  private provider: ConfigDataProvider;

  constructor() {
    const apiEndpoint = process.env.VUE_APP_API_BASE_URL;
    if (!apiEndpoint) {
      throw "API endpoint is not set";
    }
    this.provider = new ConfigDataProvider(apiEndpoint);
  }

  async addConfig(value: string): Promise<void> {
    try {
      await this.provider.addConfigLine(value);
    } catch (error) {
      if (error instanceof AxiosError && error.response?.data) {
        const responseData = error?.response?.data;
        if (responseData) {
          throw responseData;
        }
      }
      console.error("ConfigClient: Error adding config:", error);
      throw error;
    }
  }

  async getAllConfigs(): Promise<KeyValuePair[]> {
    try {
      const data = await this.provider.getConfigs();
      const result = data.map(
        (config: { id: number; key: string; value: string }) =>
          new KeyValuePair(config.id, config.key, config.value)
      );
      return result;
    } catch (error) {
      if (error instanceof AxiosError && error.response?.data) {
        const responseData = error?.response?.data;
        if (responseData) {
          throw responseData;
        }
      }
      console.error("ConfigClient: Error getting configs:", error);
      throw error;
    }
  }

  async deleteConfig(): Promise<void> {
    try {
      await this.provider.deleteConfig();
    } catch (error) {
      if (error instanceof AxiosError && error.response?.data) {
        const responseData = error?.response?.data;
        if (responseData) {
          throw responseData;
        }
      }
      console.error("ConfigClient: Error deleting configs:", error);
      throw error;
    }
  }

  async filterConfig(value: string): Promise<KeyValuePair[]> {
    try {
      const filteredConfig = await this.provider.filterConfig(value);
      const result = filteredConfig.map(
        (config: { id: number; key: string; value: string }) =>
          new KeyValuePair(config.id, config.key, config.value)
      );
      return result;
    } catch (error) {
      if (error instanceof AxiosError && error.response?.data) {
        const responseData = error?.response?.data;
        if (responseData) {
          throw responseData;
        }
      }
      console.error("ConfigClient: Error filtering configs:", error);
      throw error;
    }
  }

  async sortConfig(type: number): Promise<KeyValuePair[]> {
    try {
      const sortedConfig = await this.provider.sortConfig(type);
      const result = sortedConfig.map(
        (config: { id: number; key: string; value: string }) =>
          new KeyValuePair(config.id, config.key, config.value)
      );
      return result;
    } catch (error) {
      if (error instanceof AxiosError && error.response?.data) {
        const responseData = error?.response?.data;
        if (responseData) {
          throw responseData;
        }
      }
      console.error("ConfigClient: Error sorting configs:", error);
      throw error;
    }
  }
}

export default ConfigClient;
