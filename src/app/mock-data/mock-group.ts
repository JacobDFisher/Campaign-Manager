import { Group } from '../interfaces/group'

export const GROUPS: Group[] = [
    {
        name: "Players"
    },
    {
        name: "Player 1",
        memberOf: ["Players"]
    },
    {
        name: "Player 2",
        memberOf: ["Players"]
    },
    {
        name: "Runner"
    }
]