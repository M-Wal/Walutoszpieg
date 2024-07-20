import React, { useState } from 'react';
import UserSelect from '../components/UserSelect';
import UserWallet from '../components/UserWallet';

const UserWalletPage = () => {
    const [selectedUserId, setSelectedUserId] = useState('');

    const handleUserSelected = (userId) => {
        setSelectedUserId(userId);
    };
    return (
        <div className="main-container">
            Portfel walutowy użytkownika <UserSelect onUserSelected={handleUserSelected} />
            {selectedUserId && <UserWallet userId={selectedUserId} />}
        </div>
    );
};

export default UserWalletPage;
