import React from 'react';
import { useParams } from 'react-router-dom';
import CurrencyHistory from '../components/CurrencyHistory';

const CurrencyDetails = () => {
    const { code } = useParams();

    return (
        <div className="main-container">
            <h2>Szczegółowa historia waluty</h2>
            <CurrencyHistory code={code} />
        </div>
    );
};

export default CurrencyDetails;
