export enum SourceType{
    Entity = 0,
    Identity = 1,
    Other = 2
}

export interface Source{
    sourceType: SourceType;
    id: number;
}