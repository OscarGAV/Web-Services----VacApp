# VacApp Frontend

A modern React TypeScript frontend application for VacApp with beautiful design, smooth animations, and comprehensive bovines management system.

## ✨ Features

- **🔐 Secure Authentication** - User registration and login with JWT tokens
- **🎨 Modern Design** - Beautiful gradients, glassmorphism effects, and smooth animations
- **📱 Responsive Layout** - Works perfectly on desktop, tablet, and mobile devices
- **🛡️ Protected Routes** - Secure navigation with authentication guards
- **⚡ Fast Performance** - Built with Vite for optimal loading times
- **🎭 Smooth Animations** - Custom CSS animations and transitions
- **🌈 Beautiful UI** - Tailwind CSS with custom components and styling
- **🐄 Bovines Management** - Complete livestock registration and tracking system

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

### Authentication Pages
- **Login Page** - Modern gradient background with floating elements
- **Register Page** - Similar styling with unique color scheme and validation

### Dashboard
- **Home Dashboard** - Welcome section with user avatar and navigation cards
- **Profile Information** - User details displayed in colorful gradient cards
- **Quick Actions** - Interactive dashboard cards with hover effects
- **Account Overview** - Statistics and account information

### Bovines Management
- **Bovines List** - Grid view of all registered bovines with statistics
- **Add Bovine** - Comprehensive form to register new livestock
- **Bovine Cards** - Individual cards showing detailed information
- **Image Upload** - Support for bovine photos with preview
- **Statistics Dashboard** - Overview of total bovines, breeds, and locations

## 🚀 API Integration

This frontend connects to the VacApp backend API at:
`https://vacappv2-bxcpfqarbwgpddh8.canadacentral-01.azurewebsites.net`

### Authentication Endpoints
- `POST /api/v1/user/sign-up` - User registration
- `POST /api/v1/user/sign-in` - User login
- `GET /api/v1/user/profile` - Get user profile (protected)

### Bovines Endpoints
- `GET /api/v1/bovines` - Get all user's bovines (protected)
- `POST /api/v1/bovines` - Create new bovine (protected, multipart/form-data)

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
│   ├── Home.tsx             # Dashboard with navigation
│   ├── Bovines.tsx          # Bovines list and management
│   └── AddBovine.tsx        # Add new bovine form
├── services/
│   └── api.ts               # API service layer with all endpoints
├── App.tsx                  # Main app with routing
├── index.css                # Custom CSS with animations
└── main.tsx                 # Application entry point
```

## 🔐 Authentication Flow

1. User visits app → redirected to login if not authenticated
2. User can register or login with animated forms
3. JWT token stored securely in localStorage
4. Protected routes require valid authentication
5. Dashboard displays user profile and navigation options
6. Logout clears session and redirects to login

## 🐄 Bovines Management Features

### Registration
- **Complete Form** - Name, gender, birth date, breed, location, stable ID
- **Image Upload** - Optional photo upload with preview
- **Validation** - Real-time form validation and error handling
- **Responsive Design** - Works on all screen sizes

### Viewing & Management
- **Grid Layout** - Beautiful cards displaying bovine information
- **Detailed Information** - Name, age calculation, breed, location, etc.
- **Empty State** - Helpful message when no bovines are registered
- **Statistics** - Overview of total bovines, breeds, and locations
- **Navigation** - Easy navigation between dashboard and bovines management

### Data Display
- **Age Calculation** - Automatic age calculation from birth date
- **Image Handling** - Displays uploaded images or placeholder icons
- **Organized Information** - Clean, easy-to-read data presentation
- **Loading States** - Smooth loading indicators during API calls

## 🎨 Styling Architecture

- **Tailwind CSS** - Utility-first CSS framework
- **Custom Animations** - Keyframe animations in CSS
- **Gradient System** - Consistent color gradients throughout
- **Glass Effects** - Backdrop blur and transparency
- **Responsive Design** - Mobile-first breakpoints
- **Inter Font** - Modern typography from Google Fonts

## 🌟 Key Improvements

- **Visual Appeal** - Modern gradients and glassmorphism
- **User Experience** - Smooth animations and transitions
- **Functionality** - Complete bovines management system
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
- Form validation and file upload handling
- Image preview functionality for better UX

## 🔄 Navigation Flow

```
Login/Register → Dashboard → Bovines Management
                     ↓              ↓
                Profile Info    Add New Bovine
                     ↓              ↓
                Settings/Help   Bovines List
```

The application provides a complete livestock management solution with a beautiful, modern interface that makes it easy for users to register, view, and manage their bovines efficiently.
