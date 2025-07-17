import axios from 'axios';

const API_BASE_URL = 'https://vacappv2-bxcpfqarbwgpddh8.canadacentral-01.azurewebsites.net/api/v1';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add request interceptor to include token in headers
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export interface SignUpRequest {
  username: string;
  password: string;
  email: string;
}

export interface SignInRequest {
  userName: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  userName: string;
  email: string;
}

export interface UserProfile {
  username: string;
  email: string;
  emailConfirmed: boolean;
}

export const authApi = {
  signUp: async (data: SignUpRequest): Promise<AuthResponse> => {
    const response = await api.post('/user/sign-up', data);
    return response.data;
  },

  signIn: async (data: SignInRequest): Promise<AuthResponse> => {
    const response = await api.post('/user/sign-in', data);
    return response.data;
  },

  getProfile: async (): Promise<UserProfile> => {
    const response = await api.get('/user/profile');
    return response.data;
  },
};

export default api;