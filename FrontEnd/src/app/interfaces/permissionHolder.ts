import { Identity } from './identity';
import { Revealed } from './revealed';
import { Permission } from './permission';

export interface PermissionHolder{
    id: number;
    author: Identity;
    perms: Permission[];
    revealed: Revealed[];
}