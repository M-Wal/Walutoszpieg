import React, { useState } from 'react';
import { createUser, updateUser } from '../api/api';

const UserForm = ({ user, onUserCreated, onUserUpdated }) => {
    const [username, setUsername] = useState(user ? user.username : '');
    const [email, setEmail] = useState(user ? user.email : '');
    const [password, setPassword] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        const userData = {
            username,
            email,
            password_hash: password,
            created_at: new Date().toISOString(),
            updated_at: new Date().toISOString(),
        };
        if (user) {
            userData.id = user.id;
            await updateUser(userData);
            onUserUpdated();
        } else {
            await createUser(userData);
            onUserCreated();
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="Imię i nazwisko"
            />
            <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
            />
            <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Hasło"
            />
            <button type="submit">{user ? 'Zaktualizuj użytkownika' : 'Dodaj użytkownika'}</button>
        </form>
    );
};

export default UserForm;
