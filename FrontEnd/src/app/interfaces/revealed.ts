import { Group } from './group';
import { Source } from './source';

export interface Revealed { // 
    destinationId: number; // obtainer of knowledge
    percentage: number; // amount of group who knows this
    source: Source; // source of knowledge
}