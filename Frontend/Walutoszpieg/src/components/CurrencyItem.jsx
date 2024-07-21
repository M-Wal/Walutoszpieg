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
        <table className="table-container">
            <tbody >
                <tr>
                    <td>{currency.currency} ({currency.code}) -</td>
                    <td style={style}>
                        {lastRate.toFixed(4)} PLN
                        {!isAboveAverage ? ' - rozważ zakup' : ''}
                    </td>
                    <td >
                        <Link to={`/currency/${currency.code}`}>Zobacz historię</Link>
                    </td>
                </tr>
            </tbody>
        </table>
    );
};

export default CurrencyItem;
