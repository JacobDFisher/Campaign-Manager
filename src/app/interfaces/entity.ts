import { Detail } from './detail'
import { Revealed } from './revealed';

export interface Entity {
    id : string;
    properties: {name: string, detail: Detail}[];
    details: Detail[];
}