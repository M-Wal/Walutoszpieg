import React, { useEffect, useState } from 'react';
import { fetchUserById } from '../api/api';
import UserForm from './UserForm';

const UserEdit = ({ userId, onUserUpdated }) => {
    const [user, setUser] = useState(null);

    useEffect(() => {
        const getUser = async () => {
            try {
                const data = await fetchUserById(userId);
                setUser(data);
            } catch (error) {
                console.error('Error fetching user:', error);
            }
        };

        getUser();
    }, [userId]);

    return user ? (
        <UserForm user={user} onUserUpdated={onUserUpdated} />
    ) : (
        <p>Loading user data...</p>
    );
};

export default UserEdit;
