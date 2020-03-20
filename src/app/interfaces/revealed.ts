import { Group } from './group';

export interface Revealed { // 
    destination: string; // obtainer of knowledge
    percentage: number; // amount of group who knows this
    source: string; // source of knowledge
}