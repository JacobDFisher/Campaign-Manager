import { Entity } from '../interfaces/entity'
import { SourceType } from '../interfaces/source'

export const CHARACTERS: Entity[] = [
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
                        source: {sourceType: SourceType.Identity, id: 3},
                        destinationId: 0,
                        percentage: 100
                    }]
                }
            }],
        details: [{
            description: "Has a large family",
            author: 3,
            revealed: [{
                source: {sourceType: SourceType.Identity, id: 3},
                destinationId: 0,
                percentage: 100
            }]
        },
        {
            description: "Part of a global conspiracy",
            author: 1,
            revealed: [{
                source: {sourceType: SourceType.Identity, id: 1},
                destinationId: 3,
                percentage: 100
            }]
        }
    ],
    revealed: [{
        source: {sourceType: SourceType.Identity, id: 3},
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
                        source: {sourceType: SourceType.Identity, id: 4},
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
            source: {sourceType: SourceType.Identity, id: 4},
            destinationId: 2,
            percentage: 100
        }]
    }
]