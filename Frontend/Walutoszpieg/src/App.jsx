import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Main from './pages/Main.jsx';
import UserManagement from './pages/UserManagement';
import ExchangePage from "./pages/ExchangePage";
import UserWallet from './components/UserWallet';
import Currency from "./pages/Currency";
import CurrencyDetails from "./components/CurrencyDetails.jsx";

const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/userwallet" element={<UserWallet userId={1} />} />
                <Route path="/" element={<Main />} />
                <Route path="/users" element={<UserManagement />} />
                <Route path="/currency" element={<Currency />} />
                <Route path="/currency/:code" element={<CurrencyDetails />} />
            </Routes>
        </Router>
    );
};

export default App;
