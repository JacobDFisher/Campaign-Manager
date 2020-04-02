import { Entity } from '../interfaces/entity'

export const CHARACTERS = [
    {
        id: 1,
        name: 'Character 1',
        author: 3,
        properties: [
            {
                name: 'name',
                detail: {
                    description: 'Character 1',
                    author: 3,
                    revealed: [{
                        
                        destinationId: 0,
                        percentage: 100
                    }]
                }
            }],
        details: [{
            description: "Has a large family",
            author: 3,
            revealed: [{
                
                destinationId: 0,
                percentage: 100
            }]
        },
        {
            description: "Part of a global conspiracy",
            author: 1,
            revealed: [{
                
                destinationId: 3,
                percentage: 100
            }]
        }
    ],
    revealed: [{
        
        destinationId: 0,
        percentage: 100
    }]
    },
    {
        id: 2,
        name: 'Character 2',
        author: 4,
        properties: [
            {
                name: 'name',
                detail: {
                    description: 'Character 2',
                    author: 4,
                    revealed: [{
                        
                        destinationId: 0,
                        percentage: 100
                    }]
                }
            }],
        details: [{
            description: "Tall",
            author: 4,
            revealed: []
        }],
        revealed: [{
            
            destinationId: 2,
            percentage: 100
        }]
    }
]