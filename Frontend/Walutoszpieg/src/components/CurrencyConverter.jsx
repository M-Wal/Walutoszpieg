import React, { useEffect, useState } from 'react';
import { fetchCurrencyRates } from '../api/api';

const CurrencyConverter = () => {
    const [rates, setRates] = useState([]);
    const [fromCurrency, setFromCurrency] = useState('');
    const [toCurrency, setToCurrency] = useState('');
    const [amount, setAmount] = useState(0);
    const [convertedAmount, setConvertedAmount] = useState(null);

    useEffect(() => {
        const getRates = async () => {
            try {
                const data = await fetchCurrencyRates();
                setRates(data[0].rates);
            } catch (error) {
                console.error('Error fetching currency rates:', error);
            }
        };

        getRates();
    }, []);

    const handleConvert = () => {
        const fromRate = rates.find(rate => rate.code === fromCurrency);
        const toRate = rates.find(rate => rate.code === toCurrency);

        if (fromRate && toRate) {
            const converted = (amount * fromRate.mid) / toRate.mid;
            setConvertedAmount(converted.toFixed(2));
        }
    };

    return (
        <div className="main-container">
            <h2>Podręczny kalkulator wymiany walut</h2>
            <div>
                <input
                    type="number"
                    value={amount}
                    onChange={(e) => setAmount(e.target.value)}
                    placeholder="Kwota"
                />
                <select value={fromCurrency} onChange={(e) => setFromCurrency(e.target.value)}>
                    <option value="">Waluta bazowa</option>
                    {rates.map(rate => (
                        <option key={rate.code} value={rate.code}>{rate.currency} ({rate.code})</option>
                    ))}
                </select>
                <select value={toCurrency} onChange={(e) => setToCurrency(e.target.value)}>
                    <option value="">Waluta docelowa</option>
                    {rates.map(rate => (
                        <option key={rate.code} value={rate.code}>{rate.currency} ({rate.code})</option>
                    ))}
                </select>
                <button onClick={handleConvert}>Przelicz</button>
            </div>
            {convertedAmount !== null && (
                <div>
                    <h3>Wynik wymiany: {convertedAmount} {toCurrency}</h3>
                </div>
            )}
        </div>
    );
};

export default CurrencyConverter;
