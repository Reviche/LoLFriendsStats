// src/App.js
import React from 'react';
import FriendTabs from './components/FriendTabs';

function App() {
    const friends = [
        { name: 'Kevin', gameName: 'Revice', tagLine: '5261' },

    ];

    return (
        <div className="App">
            <FriendTabs friends={friends} />
        </div>
    );
}

export default App;
