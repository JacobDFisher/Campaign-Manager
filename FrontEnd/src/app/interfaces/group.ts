export interface Group{
    id: number;
    name?: string;
    memberOf?: Group[];
}