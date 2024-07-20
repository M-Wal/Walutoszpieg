import React, { useEffect, useState } from 'react';
import { fetchUsers, deleteUser } from '../api/api';

const UserList = ({ onUserSelected }) => {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        const getUsers = async () => {
            try {
                const data = await fetchUsers();
                setUsers(data);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        getUsers();
    }, []);

    const handleDelete = async (id) => {
        try {
            await deleteUser(id);
            setUsers(users.filter(user => user.id !== id));
        } catch (error) {
            console.error('Error deleting user:', error);
        }
    };

    return (
        <div className="main-container">
            <h2>Lista użytkowników</h2>
            <table className="table-container">
                {users.map(user => (
                    <tr key={user.id}>
                        {user.username} ({user.email})
                        <button onClick={() => onUserSelected(user)}>Edytuj</button>
                        <button onClick={() => handleDelete(user.id)}>Usuń</button>
                    </tr>
                ))}
            </table>
        </div>
    );
};

export default UserList;
