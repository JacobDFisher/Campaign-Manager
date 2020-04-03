import { Injectable } from '@angular/core';
import { BehaviorSubject, merge } from 'rxjs';
import { Entity } from '../interfaces/entity';
import { IdentityService } from './identity.service';
import { EntityService } from './entity-service.service';

@Injectable({
  providedIn: 'root'
})
export class ActiveEntitiesService {

  entityIds$: BehaviorSubject<number[]>;
  entities$: BehaviorSubject<Entity[]>;

  constructor(private identityService: IdentityService, private entityService: EntityService) {
    this.entities$ = new BehaviorSubject(<Entity[]> []);
    this.entityIds$ = new BehaviorSubject(<number[]> []);
    merge(this.identityService.identity$, this.identityService.groups$).subscribe(() => {
      this.entityService.getEntities(this.entityIds$.value).subscribe(entities => this.entities$.next(entities.map(e => this.filterEntity(e))));
    });
  }

  clear(){
    this.entities$.next([]);
    this.entityIds$.next([]);
  }

  addEntity(entity: Entity){
    let entities = this.entities$.value;
    let entIds = this.entityIds$.value;
    if (entities.filter(e => e?.id == entity.id).length == 0) {
      this.entities$.next(entities.concat(this.filterEntity(entity)));
    }
    if (!entIds.includes(entity.id)) {
      this.entityIds$.next(entIds.concat(entity.id));
    }
  }

  removeEntity(id: number) {
    let entities = this.entities$.value;
    let entIds = this.entityIds$.value;
    if (entities.filter(e => e?.id == id).length > 0) {
      this.entities$.next(entities.filter(e => e.id != id))
    }
    if (entIds.includes(id)) {
      this.entityIds$.next(entIds.filter(e => e != id))
    }
  }

  filterEntity(entity: Entity): Entity {
    debugger;
    let groups = this.identityService.groups$.getValue().map(g => g.id);
    let identity = this.identityService.identity$.getValue().id;
    if (entity.permissions.author.id == identity || [...entity.permissions.perms.map(p => p.grantee.id), ...entity.permissions.revealed.map(r => r.group.id)].filter(x => groups.includes(x)).length > 0) {
      return <Entity>{
        id: entity.id,
        name: entity.name,
        permissions: entity.permissions,
        details: entity.details.filter(d => d.permissions.author.id == identity || [...d.permissions.perms.map(p => p.grantee.id), ...d.permissions.revealed.map(r => r.group.id)].filter(x => groups.includes(x)).length > 0),
        properties: entity.properties.filter(p => p.detail.permissions.author.id == identity || [...p.detail.permissions.perms.map(p => p.grantee.id), ...p.detail.permissions.revealed.map(r => r.group.id)].filter(x => groups.includes(x)).length > 0)
      }
    } else {
      return null;
    }
  }
}
