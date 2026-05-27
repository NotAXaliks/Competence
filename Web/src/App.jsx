import LoginPage from './pages/LoginPage';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { MainLayout } from './pages/MainLayout';
import SecureLS from 'secure-ls';
import { ApiService } from './services/ApiService';
import Dashboard from './pages/Dashboard';
import Profile from './pages/Profile';
import RatingPage from './pages/RatingPage';

function App() {
  const isLoggedIn = ApiService.ls.get("token");
  if (!isLoggedIn) return (<LoginPage />);

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<Dashboard />} />
          <Route path="dashboard" element={<Dashboard />} />
          <Route path="profile" element={<Profile />} />
          <Route path="rating" element={<RatingPage />} />
        </Route>
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
