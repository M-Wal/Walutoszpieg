import React from 'react';
import CurrencyList from '../components/CurrencyList';
import CurrencyConverter from '../components/CurrencyConverter';

const ExchangePage = () => {
    return (
        <div className="main-container">
            <h1>Currency Exchange</h1>
            <CurrencyConverter />
            <CurrencyList />
        </div>
    );
};

export default ExchangePage;
