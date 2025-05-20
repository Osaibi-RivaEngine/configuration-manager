import axios, { AxiosInstance } from "axios";

class ConfigDataProvider {
  private axiosInstance: AxiosInstance;

  constructor(baseUrl: string) {
    this.axiosInstance = axios.create({
      baseURL: baseUrl,
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    });
  }

  async addConfigLine(keyValuePair: string): Promise<void> {
    try {
      await this.axiosInstance.post("api/configs/add", keyValuePair);
    } catch (error) {
      console.error("Error adding config:", error);
      throw error;
    }
  }

  async deleteConfig(): Promise<void> {
    try {
      await this.axiosInstance.delete("api/configs/delete");
    } catch (error) {
      console.error("Error deleting config:", error);
      throw error;
    }
  }

  async getConfigs(): Promise<{ id: number; key: string; value: string }[]> {
    try {
      const response = await this.axiosInstance.get("api/configs/getall");
      return response.data;
    } catch (error) {
      console.error("Error getting config:", error);
      throw error;
    }
  }

  async filterConfig(
    value: string
  ): Promise<{ id: number; key: string; value: string }[]> {
    try {
      const response = await this.axiosInstance.get(
        `api/configs/filter?filter=${value}`
      );
      return response.data;
    } catch (error) {
      console.error("Error filtering config:", error);
      throw error;
    }
  }

  async sortConfig(
    type: number
  ): Promise<{ id: number; key: string; value: string }[]> {
    try {
      const response = await this.axiosInstance.get(
        `api/configs/sort?type=${type}`
      );
      return response.data;
    } catch (error) {
      console.error("Error sorting config:", error);
      throw error;
    }
  }
}

export default ConfigDataProvider;
