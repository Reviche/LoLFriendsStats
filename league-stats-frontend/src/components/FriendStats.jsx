import React, { useState, useEffect } from 'react';
import axios from 'axios';

function FriendStats({ gameName, tagLine }) {
    const [stats, setStats] = useState(null);
    const [error, setError] = useState(null);

    const backendUrl = 'https://localhost:7074';

    useEffect(() => {
        const fetchStats = async () => {
            try {
                // Fetch summoner data by Riot ID
                const summonerResponse = await axios.get(
                    `${backendUrl}/api/League/${encodeURIComponent(gameName)}/${encodeURIComponent(tagLine)}`
                );
                const summoner = summonerResponse.data;
                const { id: summonerId, puuid } = summoner;

                // Fetch last 20 match IDs
                const matchesResponse = await axios.get(
                    `${backendUrl}/api/League/${puuid}/matches?count=20`
                );
                const matchIds = matchesResponse.data;

                let wins = 0;
                let losses = 0;
                const championCounts = {};

                // Fetch match details
                for (const matchId of matchIds) {
                    const matchResponse = await axios.get(`${backendUrl}/api/League/matches/${matchId}`);
                    const match = matchResponse.data;

                    // Find participant data
                    const participant = match.info.participants.find((p) => p.puuid === puuid);

                    if (participant) {
                        participant.win ? wins++ : losses++;

                        const champion = participant.championName;
                        championCounts[champion] = (championCounts[champion] || 0) + 1;
                    }
                }

                const mostPlayedChampion = Object.keys(championCounts).reduce((a, b) =>
                    championCounts[a] > championCounts[b] ? a : b
                );

                const winRate = ((wins / (wins + losses)) * 100).toFixed(2);

                setStats({
                    mostPlayedChampion,
                    wins,
                    losses,
                    winRate,
                });
            } catch (err) {
                console.error(err);
                setError('Failed to fetch stats.');
            }
        };

        fetchStats();
    }, [gameName, tagLine]);

    if (error) return <p>{error}</p>;
    if (!stats) return <p>Loading stats...</p>;

    return (
        <div>
            <h2>{gameName}'s Stats</h2>
            <p>Most Played Champion: {stats.mostPlayedChampion}</p>
            <p>Wins: {stats.wins}</p>
            <p>Losses: {stats.losses}</p>
            <p>Win Rate: {stats.winRate}%</p>
        </div>
    );
}

export default FriendStats;
