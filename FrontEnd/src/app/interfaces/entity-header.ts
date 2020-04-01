import { Revealed } from './revealed';

export interface EntityHeader {
    id : number;
    name: string;
    author: number;
    revealed?: Revealed[];
}