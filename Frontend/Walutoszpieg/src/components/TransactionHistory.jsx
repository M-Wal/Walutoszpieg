import React, { useState, useEffect } from 'react';
import { format, parseISO } from 'date-fns';
import { fetchTransactionHistories, fetchUsers } from '../api/api';

const TransactionHistory = () => {
    const [transactions, setTransactions] = useState([]);
    const [filteredTransactions, setFilteredTransactions] = useState([]);
    const [users, setUsers] = useState([]);
    const [selectedUser, setSelectedUser] = useState('');
    const [currencyFilter, setCurrencyFilter] = useState('');
    const [dateFilter, setDateFilter] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            const transactionResult = await fetchTransactionHistories();
            setTransactions(transactionResult);
            setFilteredTransactions(transactionResult);

            const userResult = await fetchUsers();
            setUsers(userResult);
        };

        fetchData();
    }, []);

    const handleFilter = () => {
        let filtered = transactions;
        if (selectedUser) {
            filtered = filtered.filter(transaction => transaction.userId === parseInt(selectedUser));
        }
        if (currencyFilter) {
            filtered = filtered.filter(transaction =>
                transaction.fromCurrency.includes(currencyFilter) ||
                transaction.toCurrency.includes(currencyFilter)
            );
        }
        if (dateFilter) {
            filtered = filtered.filter(transaction =>
                format(parseISO(transaction.timestamp), 'yyyy-MM-dd') === dateFilter
            );
        }
        setFilteredTransactions(filtered);
    };

    useEffect(() => {
        handleFilter();
    }, [selectedUser, currencyFilter, dateFilter]);

    return (
        <div className="main-container">
            <h2>Historia transakcji wymiany walut</h2>
            <div>
                <h4>Filtrowanie wyników:</h4>
                <label>
                    Użytkownik:
                    <select value={selectedUser} onChange={(e) => setSelectedUser(e.target.value)}>
                        <option value="">Wybierz użytkownika</option>
                        {users.map(user => (
                            <option key={user.id} value={user.id}>{user.username}</option>
                        ))}
                    </select>
                </label>
                <label>
                    Waluta:
                    <input
                        type="text"
                        value={currencyFilter}
                        onChange={(e) => setCurrencyFilter(e.target.value.toUpperCase())}
                    />
                </label>
                <label>
                    Data:
                    <input
                        type="date"
                        value={dateFilter}
                        onChange={(e) => setDateFilter(e.target.value)}
                    />
                </label>
            </div>
            <br />
            <table className="table-container">
                <tbody className="table">
                    <tr>
                        <th>Waluta bazowa</th>
                        <th>Waluta docelowa</th>
                        <th>Kwota</th>
                        <th>Kurs wymiany</th>
                        <th>Data wymiany</th>
                    </tr>
                
                    {filteredTransactions.map(transaction => (
                        <tr key={transaction.id}>
                            <td>{transaction.fromCurrency}</td>
                            <td>{transaction.toCurrency}</td>
                            <td>{transaction.amount.toFixed(2)}</td>
                            <td>{transaction.rate.toFixed(4)}</td>
                            <td>{format(parseISO(transaction.timestamp), 'yyyy-MM-dd HH:mm:ss')}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default TransactionHistory;
