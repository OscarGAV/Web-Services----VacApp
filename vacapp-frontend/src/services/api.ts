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

export interface Bovine {
  id: number;
  name: string;
  gender: string;
  birthDate: string;
  breed: string;
  location: string;
  bovineImg: string;
  stableId: number;
}

export interface CreateBovineRequest {
  name: string;
  gender: string;
  birthDate: string;
  breed: string;
  location: string;
  bovineImg?: File;
  stableId: number;
}

export interface UpdateBovineRequest {
  name: string;
  gender: string;
  birthDate: string;
  breed: string;
  location: string;
  bovineImg?: File;
  stableId: number;
}

export interface UpdateProfileRequest {
  username: string;
  email: string;
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

  updateProfile: async (data: UpdateProfileRequest): Promise<void> => {
    const response = await api.put('/user/update-profile', data);
    return response.data;
  },

  deleteAccount: async (): Promise<void> => {
    const response = await api.delete('/user/delete-account');
    return response.data;
  },
};

export const bovinesApi = {
  getAllBovines: async (): Promise<Bovine[]> => {
    const response = await api.get('/bovines');
    return response.data;
  },

  createBovine: async (data: CreateBovineRequest): Promise<Bovine> => {
    const formData = new FormData();
    formData.append('Name', data.name);
    formData.append('Gender', data.gender);
    formData.append('BirthDate', data.birthDate);
    formData.append('Breed', data.breed);
    formData.append('Location', data.location);
    formData.append('StableId', data.stableId.toString());
    
    if (data.bovineImg) {
      formData.append('FileData', data.bovineImg);
    }

    const response = await api.post('/bovines', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },

  getBovineById: async (id: number): Promise<Bovine> => {
    const response = await api.get(`/bovines/${id}`);
    return response.data;
  },

  updateBovine: async (id: number, data: UpdateBovineRequest): Promise<Bovine> => {
    const formData = new FormData();
    formData.append('Name', data.name);
    formData.append('Gender', data.gender);
    formData.append('BirthDate', data.birthDate);
    formData.append('Breed', data.breed);
    formData.append('Location', data.location);
    formData.append('StableId', data.stableId.toString());
    
    if (data.bovineImg) {
      formData.append('FileData', data.bovineImg);
    }

    const response = await api.put(`/bovines/${id}`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },

  deleteBovine: async (id: number): Promise<void> => {
    const response = await api.delete(`/bovines/${id}`);
    return response.data;
  },
};

export default api;