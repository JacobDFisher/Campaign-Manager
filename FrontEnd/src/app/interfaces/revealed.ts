import { Group } from './group';
import { Entity } from './entity';

export interface Revealed { // 
    group: Group; // obtainer of knowledge
    percentage: number; // amount of group who knows this
    source: Entity; // source of knowledge
}