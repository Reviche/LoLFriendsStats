import React, { useState } from 'react';
import { Tabs, Tab, Box } from '@mui/material';
import FriendStats from './FriendStats';

function FriendTabs({ friends }) {
    const [selectedTab, setSelectedTab] = useState(0);

    const handleChange = (event, newValue) => {
        setSelectedTab(newValue);
    };

    return (
        <Box sx={{ width: '100%' }}>
            <Tabs value={selectedTab} onChange={handleChange} aria-label="friend tabs">
                {friends.map((friend, index) => (
                    <Tab label={friend.name} key={index} />
                ))}
            </Tabs>
            <Box sx={{ padding: 2 }}>
                <FriendStats gameName={friends[selectedTab].gameName} tagLine={friends[selectedTab].tagLine} />
            </Box>
        </Box>
    );
}

export default FriendTabs;
