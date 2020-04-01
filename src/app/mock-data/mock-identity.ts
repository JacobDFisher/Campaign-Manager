import { Identity } from '../interfaces/identity';

export const IDENTITIES: Identity[] = [
    {
        id: 0,
        name: "Creator"
    },
    {
        id: 1,
        name: "Runner",
        groups: [1]
    },
    {
        id: 3,
        name: "Player 1",
        groups: [3]
    },
    {
        id: 4,
        name: "Player 2",
        groups: [4]
    },
    {
        name: "Spectator"
    }
]