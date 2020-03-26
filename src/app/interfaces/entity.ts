import { Detail } from './detail'
import { Property } from './property';
import { Revealed } from './revealed';

export interface Entity {
    id : number;
    name: string;
    author: number;
    revealed?: Revealed[];
    properties: Property[];
    details: Detail[];
}