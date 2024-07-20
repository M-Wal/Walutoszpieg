import React, { useEffect, useState } from 'react';
import { fetchCurrencyHistory } from '../api/api';

const CurrencyHistory = ({ code }) => {
    const [history, setHistory] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await fetchCurrencyHistory(code);
            setHistory(data);
        };

        fetchData();
    }, [code]);

    return (
        <div className="main-container">
            <h3>Kursy waluty {code} w przeliczaniu na 1 PLN</h3>
            <ul>
                {history.map(rate => (
                    <li key={rate.effectiveDate}>
                        {rate.effectiveDate}: {rate.mid.toFixed(4)} PLN
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default CurrencyHistory;
