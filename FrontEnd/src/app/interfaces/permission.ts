import { Identity } from './identity';
import { Group } from './group';

export enum PermissionType{
    Owner = 0,
    Editor = 1,
    Viewer = 2
}

export interface Permission{
    permissionType: PermissionType;
    grantor: Identity;
    grantee: Group;
}