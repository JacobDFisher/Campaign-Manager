import { Group } from './group';

export interface Identity{
    id?: number;
    name: string;
    groups?: Group[];
}