import React from 'react';
import CurrencyList from "../components/CurrencyList.jsx";
import CurrencyConverter from "../components/CurrencyConverter.jsx";

const CurrencyPage = () => {
    return (
        <div className="main-container">
            <CurrencyConverter />
            <h2>Lista walut - trendy (z rekomendacją)</h2>
            <div> rekomendacja wystawiona na postawie uśrednienia 5 ostatnich kursów i porównania z kursem bieżącym</div>
            <CurrencyList />
        </div>
    );
};

export default CurrencyPage;