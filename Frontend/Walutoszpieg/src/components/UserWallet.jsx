import React, { useEffect, useState } from 'react';
import { getUserWallets, addOrUpdateWallet, deleteWallet, convertCurrency, fetchCurrencyRates, createTransaction } from '../api/api';

const UserWallet = ({ userId }) => {
    const [wallets, setWallets] = useState([]);
    const [currencyRates, setCurrencyRates] = useState([]);
    const [newCurrency, setNewCurrency] = useState('');
    const [newAmount, setNewAmount] = useState(0);
    const [fromCurrency, setFromCurrency] = useState('');
    const [toCurrency, setToCurrency] = useState('');
    const [convertAmount, setConvertAmount] = useState(0);
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

        if (!newCurrency) {
            setError('Nie wybrano waluty');
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

        if (!fromCurrency || !toCurrency) {
            setError('Nie wybrano waluty');
            return;
        }

        if (!fromWallet || fromWallet.amount < convertAmount) {
            setError('Niewystarczające środki w wybranej walucie na wykonanie transakcji');
            return;
        }

        const fromRate = currencyRates.find(rate => rate.code === fromCurrency).mid;
        const toRate = currencyRates.find(rate => rate.code === toCurrency).mid;
        const converted = parseFloat(convertAmount) * (fromRate / toRate);
        setError('');

        const request = {
            userId,
            fromCurrency,
            toCurrency,
            amount: parseFloat(convertAmount)
        };

        await convertCurrency(request);

        const transaction = {
            userId,
            fromCurrency,
            toCurrency,
            amount: parseFloat(convertAmount),
            rate: fromRate / toRate,
            timestamp: new Date().toISOString()
        };
        await createTransaction(transaction);

        const updatedWallets = await getUserWallets(userId);
        setWallets(updatedWallets);
        setConvertedAmount(converted.toFixed(2));
    };

    return (
        <div>
            <br />
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
            <table className="table-container">
                <tbody className="table">
                    <tr>
                        <th>Waluta</th>
                        <th>Kwota</th>
                        <th>Akcja</th>
                    </tr>
                    {wallets.map(wallet => (
                        <tr key={wallet.currencyCode}>
                            <td>{wallet.currencyCode}</td>
                            <td>{wallet.amount}</td>
                            <td>
                                <button onClick={() => handleDeleteWallet(wallet.currencyCode)}>Usuń</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div>
                <h3>Wymiana walut</h3>
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
                    <div>
                        <p style={{ color: 'red' }}>{error}</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default UserWallet;
