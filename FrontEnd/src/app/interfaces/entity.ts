import { Detail } from './detail'
import { Property } from './property';
import { PermissionHolder } from './permissionHolder';

export interface Entity {
    id : number;
    name: string;
    permissions: PermissionHolder;
    properties: Property[];
    details: Detail[];
}