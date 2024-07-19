import React from 'react';
import { Link } from 'react-router-dom';

const CurrencyItem = ({ currency }) => {
    const rates = currency.rates.map(rate => rate.mid);
    const lastRate = rates[rates.length - 1];
    const averageRate = rates.reduce((sum, rate) => sum + rate, 0) / rates.length;
    const isAboveAverage = lastRate > averageRate;

    const style = {
        color: isAboveAverage ? 'red' : 'green',
    };

    return (
        <div>
            <h3>{currency.currency} ({currency.code})</h3>
            <p style={style}>
                {lastRate.toFixed(4)} PLN
                {!isAboveAverage ? ' - rozważ zakup' : ''}
            </p>
            <Link to={`/currency/${currency.code}`}>View History</Link>
        </div>
    );
};

export default CurrencyItem;
