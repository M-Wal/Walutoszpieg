import './App.css';
import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Main from './pages/Main.jsx';
import UserManagementPage from './pages/UserManagementPage.jsx';
import CurrencyPage from "./pages/CurrencyPage.jsx";
import CurrencyDetails from "./components/CurrencyDetails.jsx";
import TransactionHistory from "./components/TransactionHistory.jsx";
import UserWalletPage from "./pages/UserWalletPage.jsx";

const App = () => {
    return (
        <Router>
            <div className="app">
                <header className="header">
                    <nav>
                        <ul className="nav-links">
                            <li><Link to="/">Główna</Link></li>
                            <li><Link to="/currency"> Lista walut - trendy</Link></li>
                            <li><Link to="/wallet">Portfela użytkownika</Link></li>
                            <li><Link to="/history">Historia transakcji wymiany walut</Link></li>
                            <li style={{ color: 'white' }}>Zarządzanie użytkownikami</li>
                        </ul>
                    </nav>
                </header>
                <main className="main-content">
                    <Routes>
                        <Route path="/" element={<Main />} />
                        <Route path="/users" element={<UserManagementPage />} />
                        <Route path="/wallet" element={<UserWalletPage />} />
                        <Route path="/currency" element={<CurrencyPage />} />
                        <Route path="/currency/:code" element={<CurrencyDetails />} />
                        <Route path="/history" element={<TransactionHistory />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
};

export default App;