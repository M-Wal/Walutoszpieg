import React from 'react';
import { Link } from 'react-router-dom';
import UserSelect from '../components/UserSelect';
import ExchangePage from './ExchangePage';

const Main = () => {
    return (
        <div>
            <h1>Strona główna Walutoszpiega</h1>
            <Link to="/users"> Zarządzanie użytkownikami </Link>
            <UserSelect onUserSelected={(userId, userName, email) => console.log('User selected:', userId, userName, email)} />
            <Link to="/userwallet" > Idź do portfela użytkownika </Link>
            <Link to="/currency" > Lista walut - trendy </Link>
        </div>
    );
};

export default Main;
