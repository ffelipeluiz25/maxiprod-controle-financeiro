import axios, { type AxiosInstance, type AxiosError } from 'axios';

class ApiClient {
    private client: AxiosInstance;

    constructor() {
        this.client = axios.create({
            //baseURL: 'https://localhost:44300/api',
            baseURL: 'https://localhost:44308/api',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        this.setupInterceptors();
    }

    private setupInterceptors() {
        this.client.interceptors.response.use(
            (response) => response,
            (error: AxiosError) => {
                const message = error.response?.data || error.message;
                console.error('API Error:', message);
                return Promise.reject(error);
            }
        );
    }

    public getClient(): AxiosInstance {
        return this.client;
    }
}

export const api = new ApiClient().getClient();