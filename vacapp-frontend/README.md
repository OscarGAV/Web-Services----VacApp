# VacApp Frontend

A React TypeScript frontend application for VacApp that provides user authentication and a dashboard interface.

## Features

- User Registration
- User Login
- Protected Routes
- User Dashboard
- Responsive Design with Tailwind CSS

## API Integration

This frontend connects to the VacApp backend API at:
`https://vacappv2-bxcpfqarbwgpddh8.canadacentral-01.azurewebsites.net`

### Endpoints Used

- `POST /api/v1/user/sign-up` - User registration
- `POST /api/v1/user/sign-in` - User login
- `GET /api/v1/user/profile` - Get user profile (protected)

## Setup and Installation

1. Install dependencies:
```bash
npm install
```

2. Start the development server:
```bash
npm run dev
```

3. Build for production:
```bash
npm run build
```

## Project Structure

```
src/
├── components/
│   └── ProtectedRoute.tsx    # Route protection component
├── context/
│   └── AuthContext.tsx       # Authentication context
├── pages/
│   ├── Login.tsx            # Login page
│   ├── Register.tsx         # Registration page
│   └── Home.tsx             # Dashboard/Home page
├── services/
│   └── api.ts               # API service layer
├── App.tsx                  # Main application component
└── main.tsx                 # Application entry point
```

## Authentication Flow

1. User can register with username, email, and password
2. User can login with username and password
3. JWT token is stored in localStorage
4. Protected routes require authentication
5. User profile is fetched and displayed on the dashboard
6. User can logout to clear session

## Technologies Used

- React 18
- TypeScript
- Vite
- React Router DOM
- Axios
- Tailwind CSS
- Context API for state management
