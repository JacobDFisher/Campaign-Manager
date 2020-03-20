import { Entity } from '../interfaces/entity'

export const CHARACTERS: Entity[] = [
    {
        id: 'Character 1',
        properties: [
            {
                name: 'name',
                detail: {
                    description: 'Character 1',
                    author: 'Player 1',
                    revealed: [{
                        source: "Player 1",
                        destination: "All",
                        percentage: 100
                    }]
                }
            }],
        details: [{
            description: "Has a large family",
            author: "Player 1",
            revealed: [{
                source: "Player 1",
                destination: "All",
                percentage: 100
            }]
        },
        {
            description: "Part of a global conspiracy",
            author: "Runner",
            revealed: [{
                source: "Runner",
                destination: "Player 1",
                percentage: 100
            }]
        }
    ]
    },
    {
        id: 'Character 2',
        properties: [
            {
                name: 'name',
                detail: {
                    description: 'Character 2',
                    author: 'Player 2',
                    revealed: [{
                        source: "Player 2",
                        destination: "All",
                        percentage: 100
                    }]
                }
            }],
        details: [{
            description: "Tall",
            author: 'Player 2',
            revealed: []
        }]
    }
]