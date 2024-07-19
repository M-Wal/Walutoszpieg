import React, { useEffect, useState } from 'react';
import { fetchUsers } from '../api/api';

const UserSelect = ({ onUserSelected }) => {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        const getUsers = async () => {
            try {
                const data = await fetchUsers();
                setUsers(data);
                console.log(fetchUsers());
            } catch (error) {
                console.error('Error fetching users from api:', error);
                console.log(fetchUsers())
            }
        };

        getUsers();
    }, []);

    return (
        <select onChange={(e) => onUserSelected(e.target.value)}>
            <option value="">Wybierz użytkownika</option>
            {users.map((user) => (
                <option key={user.id} value={user.id}>
                    {user.username}
                </option>
            ))}
        </select>
    );
};

export default UserSelect;
