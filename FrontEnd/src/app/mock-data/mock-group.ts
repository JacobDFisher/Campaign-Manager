import { Group } from '../interfaces/group'

export const GROUPS: Group[] = [
    {
        name: "All",
        id: 0
    },
    {
        id: 1,
        name: "Runner"
    },
    {
        id: 2,
        name: "Players"
    },
    {
        id: 3,
        name: "Player 1",
        memberOf: [2]
    },
    {
        id: 4,
        name: "Player 2",
        memberOf: [2]
    }
    
]