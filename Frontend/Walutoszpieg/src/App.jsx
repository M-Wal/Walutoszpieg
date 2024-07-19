import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Main from './pages/Main.jsx';
import UserManagement from './pages/UserManagement';
import UserWallet from './components/UserWallet';
import Currency from "./pages/Currency";
import CurrencyDetails from "./components/CurrencyDetails.jsx";
import TransactionHistory from "./components/TransactionHistory.jsx";

const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/userwallet" element={<UserWallet userId={1} />} />
                <Route path="/" element={<Main />} />
                <Route path="/users" element={<UserManagement />} />
                <Route path="/currency" element={<Currency />} />
                <Route path="/currency/:code" element={<CurrencyDetails />} />
                <Route path="/history" element={<TransactionHistory />} />
            </Routes>
        </Router>
    );
};

export default App;
