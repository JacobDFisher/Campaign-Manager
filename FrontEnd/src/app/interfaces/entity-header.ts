import { PermissionHolder } from './permissionHolder';

export interface EntityHeader {
    id : number;
    name: string;
    permissions: PermissionHolder;
}