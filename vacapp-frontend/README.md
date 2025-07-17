# VacApp Frontend

A modern React TypeScript frontend application for VacApp with beautiful design, smooth animations, and an intuitive user interface.

## ✨ Features

- **🔐 Secure Authentication** - User registration and login with JWT tokens
- **🎨 Modern Design** - Beautiful gradients, glassmorphism effects, and smooth animations
- **📱 Responsive Layout** - Works perfectly on desktop, tablet, and mobile devices
- **🛡️ Protected Routes** - Secure navigation with authentication guards
- **⚡ Fast Performance** - Built with Vite for optimal loading times
- **🎭 Smooth Animations** - Custom CSS animations and transitions
- **🌈 Beautiful UI** - Tailwind CSS with custom components and styling

## 🎨 Design Features

- **Gradient Backgrounds** - Beautiful color gradients throughout the app
- **Glassmorphism Effects** - Modern glass-like transparency effects
- **Custom Animations** - Shake, float, slide, and fade animations
- **Interactive Elements** - Hover effects and smooth transitions
- **Modern Typography** - Inter font family for clean, readable text
- **Custom Scrollbar** - Styled scrollbar with gradient colors
- **Loading States** - Beautiful loading screens with animations
- **Responsive Design** - Mobile-first approach with breakpoints

## 📱 Pages

### Login Page
- Modern gradient background with floating elements
- Animated logo and form elements
- Real-time validation and error handling
- Smooth transitions and hover effects

### Register Page
- Similar styling to login with unique color scheme
- Form validation with helpful error messages
- Password strength indicators
- Animated submit button with loading states

### Dashboard (Home)
- Welcome section with user avatar
- Profile information cards with gradients
- Interactive dashboard cards with hover effects
- Account overview with statistics
- Sticky navigation with glassmorphism

## 🚀 API Integration

This frontend connects to the VacApp backend API at:
`https://vacappv2-bxcpfqarbwgpddh8.canadacentral-01.azurewebsites.net`

### Endpoints Used

- `POST /api/v1/user/sign-up` - User registration
- `POST /api/v1/user/sign-in` - User login
- `GET /api/v1/user/profile` - Get user profile (protected)

## 🛠️ Setup and Installation

1. **Install dependencies:**
```bash
npm install
```

2. **Start development server:**
```bash
npm run dev
```

3. **Build for production:**
```bash
npm run build
```

4. **Preview production build:**
```bash
npm run preview
```

5. **Using the startup script:**
```bash
./start.sh
```

## 📁 Project Structure

```
src/
├── components/
│   └── ProtectedRoute.tsx    # Route protection with loading screen
├── context/
│   └── AuthContext.tsx       # Authentication state management
├── pages/
│   ├── Login.tsx            # Beautiful login page
│   ├── Register.tsx         # Modern registration page
│   └── Home.tsx             # Dashboard with user profile
├── services/
│   └── api.ts               # API service layer with interceptors
├── App.tsx                  # Main app with routing
├── index.css                # Custom CSS with animations
└── main.tsx                 # Application entry point
```

## 🔐 Authentication Flow

1. User visits app → redirected to login if not authenticated
2. User can register or login with animated forms
3. JWT token stored securely in localStorage
4. Protected routes require valid authentication
5. Dashboard displays user profile and features
6. Logout clears session and redirects to login

## 🎨 Styling Architecture

- **Tailwind CSS** - Utility-first CSS framework
- **Custom Animations** - Keyframe animations in CSS
- **Gradient System** - Consistent color gradients
- **Glass Effects** - Backdrop blur and transparency
- **Responsive Design** - Mobile-first breakpoints
- **Inter Font** - Modern typography from Google Fonts

## 🌟 Key Improvements

- **Visual Appeal** - Modern gradients and glassmorphism
- **User Experience** - Smooth animations and transitions
- **Accessibility** - Proper contrast and focus states
- **Performance** - Optimized assets and lazy loading
- **Responsiveness** - Works on all screen sizes
- **Interactivity** - Hover effects and loading states

## 🚀 Technologies Used

- **React 18** - Modern React with hooks
- **TypeScript** - Type safety and better development experience
- **Vite** - Fast build tool and development server
- **React Router DOM** - Client-side routing
- **Axios** - HTTP client for API calls
- **Tailwind CSS** - Utility-first CSS framework
- **Context API** - State management for authentication
- **Google Fonts** - Inter font family

## 🎯 Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Mobile browsers (iOS Safari, Chrome Mobile)

## 📝 Development Notes

- All animations are optimized for performance
- CSS custom properties used for consistent theming
- Responsive design tested on multiple devices
- Error handling with user-friendly messages
- Loading states for better user experience
