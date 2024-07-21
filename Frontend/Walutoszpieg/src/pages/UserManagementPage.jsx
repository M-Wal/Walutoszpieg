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
        setSelectedUser(null); 
    };

    const handleUserUpdated = () => {
        setSelectedUser(null); 
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
