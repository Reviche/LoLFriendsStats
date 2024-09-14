// src/App.js
import React from 'react';
import FriendTabs from './components/FriendTabs';

function App() {
    const friends = [
        { name: 'Kevin', gameName: 'Revice', tagLine: '5261' },
        //{ name: 'Friend2', gameName: 'SummonerName2', tagLine: 'EUW1' },
        // Add more friends with gameName and tagLine
    ];

    return (
        <div className="App">
            <FriendTabs friends={friends} />
        </div>
    );
}

export default App;
