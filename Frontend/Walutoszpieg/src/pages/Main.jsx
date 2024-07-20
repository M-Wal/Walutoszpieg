import React, { useState } from 'react';
import UserSelect from '../components/UserSelect';
import UserWallet from '../components/UserWallet';

const Main = () => {
    const [selectedUserId, setSelectedUserId] = useState('');

    const handleUserSelected = (userId) => {
        setSelectedUserId(userId);
    };
    return (
        <div className="main-container">
            <h2>Twój Walutoszpieg do usług!</h2>
            <div className="content-wrapper">
                <img src="/walutoszpieg_.jpg" alt="Main Visual" className="main-image" />
                <div className="text-content">
                    <p>Sledź i zarządaj walutami w łatwy sposób. Sprawdzaj bieżące kursy aby podjąć właściwe decyzje.</p>
                    <p>W aplikacji znajdziesz:</p>
                    <ul>
                        <li>Aktualne kursy walut</li>
                        <li>Skorzystasz z krótkoterminowej analizy kursów walut</li>
                        <li>Stworzysz swój portfel walutowy, którym możesz zarządzać</li>
                        <li>Sprawdzisz historię wymian</li>
                    </ul>
                </div>
            </div>
            <p>Dobrego użytkowania :)</p>
        </div>
    );
};

export default Main;
