import React, { useState, useEffect } from 'react';
import CurrencyItem from './CurrencyItem';
import {fetchCurrencyRatesDays} from "../api/api";

const CurrencyList = () => {
    const [currencies, setCurrencies] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await fetchCurrencyRatesDays();
            const transformedData = data[0].rates.map(rate => {
                const rates = data.map(day => day.rates.find(r => r.code === rate.code));
                return {
                    currency: rate.currency,
                    code: rate.code,
                    rates,
                };
            });
            setCurrencies(transformedData);
        };

        fetchData();
    }, []);

    return (
        <div>
            {currencies.map(currency => (
                <CurrencyItem key={currency.code} currency={currency} />
            ))}
        </div>
    );
};

export default CurrencyList;
