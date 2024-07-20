import React, { useState } from 'react';
import UserList from '../components/UserList';
import UserForm from '../components/UserForm';
import UserEdit from '../components/UserEdit';

const UserManagementPage = () => {
    const [selectedUser, setSelectedUser] = useState(null);

    const handleUserSelected = (user) => {
        setSelectedUser(user);
    };

    const handleUserCreated = () => {
        setSelectedUser(null); // Reset form after user is created
    };

    const handleUserUpdated = () => {
        setSelectedUser(null); // Reset form after user is updated
    };

    return (
        <div className="main-container">
            <h1>Zarządzanie użytkownikami</h1>
            <UserForm onUserCreated={handleUserCreated} />
            <UserList onUserSelected={handleUserSelected} />
            {selectedUser && <UserEdit userId={selectedUser.id} onUserUpdated={handleUserUpdated} />}
        </div>
    );
};

export default UserManagementPage;
