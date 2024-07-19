import React from 'react';
import CurrencyList from "../components/CurrencyList.jsx";
import CurrencyConverter from "../components/CurrencyConverter.jsx";

const Currency = () => {
    return (
        <div>
            <CurrencyConverter />
            <h1>Lista walut - trendy (z rekomendacją)</h1>
            <p> rekomendacja wystawiona na postawie uśrednienia 5 ostatnich kursów i porównania z kursem bieżącym</p>
            <CurrencyList />
        </div>
    );
};

export default Currency;