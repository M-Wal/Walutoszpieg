import React, { useEffect, useState } from 'react';
import { getUserWallets, addOrUpdateWallet, deleteWallet, convertCurrency } from '../api/api';
import { fetchCurrencyRates } from '../api/api';

const UserWallet = ({ userId }) => {
    const [wallets, setWallets] = useState([]);
    const [currencyRates, setCurrencyRates] = useState([]);
    const [newCurrency, setNewCurrency] = useState('');
    const [newAmount, setNewAmount] = useState(undefined);
    const [fromCurrency, setFromCurrency] = useState('');
    const [toCurrency, setToCurrency] = useState('');
    const [convertAmount, setConvertAmount] = useState(undefined);
    const [convertedAmount, setConvertedAmount] = useState(null);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            const wallets = await getUserWallets(userId);
            setWallets(wallets);
            const rates = await fetchCurrencyRates();
            setCurrencyRates(rates[0].rates);
        };

        fetchData();
    }, [userId]);

    const handleAddOrUpdateWallet = async () => {
        const existingWallet = wallets.find(wallet => wallet.currencyCode === newCurrency);
        const newTotalAmount = existingWallet ? existingWallet.amount + parseFloat(newAmount) : parseFloat(newAmount);

        if (newTotalAmount < 0) {
            setError('Wartość waluty w portfelu nie może być ujemna !');
            return;
        }

        const wallet = { userId, currencyCode: newCurrency, amount: parseFloat(newAmount) };
        await addOrUpdateWallet(wallet);
        const updatedWallets = await getUserWallets(userId);
        setWallets(updatedWallets);
        setError('');
    };

    const handleDeleteWallet = async (currencyCode) => {
        await deleteWallet(userId, currencyCode);
        setWallets(wallets.filter(wallet => wallet.currencyCode !== currencyCode));
    };

    const handleConvertCurrency = async () => {

        const fromWallet = wallets.find(wallet => wallet.currencyCode === fromCurrency);
        if (!fromWallet || fromWallet.amount < convertAmount) {
            setError('Niewystarczające środki w wybranej walucie na wykonanie transakcji');
            return;
        }

        setError('');
        const request = {
            userId,
            fromCurrency,
            toCurrency,
            amount: parseFloat(convertAmount)
        };

        await convertCurrency(request);
        const updatedWallets = await getUserWallets(userId);
        setWallets(updatedWallets);
        const converted = request.amount * (currencyRates.find(rate => rate.code === fromCurrency).mid / currencyRates.find(rate => rate.code === toCurrency).mid)
        setConvertedAmount(parseFloat(converted.toFixed(2)));
    };

    return (
        <div>
            <h2>Portfel walutowy użytkownika {userId}</h2>
            <div>
                <input
                    type="number"
                    placeholder="Wprowadź kwotę"
                    value={newAmount}
                    onChange={(e) => setNewAmount(e.target.value)}
                />
                <select value={newCurrency} onChange={(e) => setNewCurrency(e.target.value)}>
                    <option value="">Wybierz walutę</option>
                    {currencyRates.map(rate => (
                        <option key={rate.code} value={rate.code}>{rate.code} ({rate.currency})</option>
                    ))}
                </select>

                <button onClick={handleAddOrUpdateWallet}>Dodaj walutę/zaktualizuj stan portfela</button>
            </div>
            <ul>
                {wallets.map(wallet => (
                    <li key={wallet.currencyCode}>
                        {wallet.currencyCode}: {wallet.amount}
                        <button onClick={() => handleDeleteWallet(wallet.currencyCode)}>Usuń</button>
                    </li>
                ))}
            </ul>
            <div>
                <h3>Wymiana walut</h3>-
                <select value={fromCurrency} onChange={(e) => setFromCurrency(e.target.value)}>
                    <option value="">Waluta bazowa</option>
                    {wallets.map(wallet => (
                        <option key={wallet.currencyCode} value={wallet.currencyCode}>{wallet.currencyCode}</option>
                    ))}
                </select>
                <input
                    type="number"
                    placeholder="Podaj kwotę waluty bazowej"
                    value={convertAmount}
                    onChange={(e) => setConvertAmount(e.target.value)}
                />
                <select value={toCurrency} onChange={(e) => setToCurrency(e.target.value)}>
                    <option value="">Waluta docelowa</option>
                    {currencyRates.map(rate => (
                        <option key={rate.code} value={rate.code}>{rate.code} ({rate.currency})</option>
                    ))}
                </select>

                <button onClick={handleConvertCurrency}>Wymień</button>
                {convertedAmount !== null && (
                    <div>
                        <p>Kupiono: {convertedAmount} {toCurrency} </p>
                    </div>
                )}
                {error && (
                    <div style={{color: 'red'}}>
                        <p>{error}</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default UserWallet;
