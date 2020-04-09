import { Injectable } from '@angular/core';
import { BehaviorSubject, merge } from 'rxjs';
import { Entity } from '../interfaces/entity';
import { IdentityService } from './identity.service';
import { EntityService } from './entity.service';
import { EntityHeader } from '../interfaces/entity-header';

@Injectable({
  providedIn: 'root'
})
export class ActiveEntitiesService {

  entityHeaders$: BehaviorSubject<EntityHeader[]>;
  entityIds$: BehaviorSubject<number[]>;
  entities$: BehaviorSubject<Entity[]>;

  constructor(private identityService: IdentityService, private entityService: EntityService) {
    this.entities$ = new BehaviorSubject(<Entity[]> []);
    this.entityIds$ = new BehaviorSubject(<number[]> []);
    this.entityHeaders$ = new BehaviorSubject(<EntityHeader[]> []);
    //let refreshTrigger = merge(this.identityService.identity$, this.identityService.groups$);
    let refreshTrigger = this.identityService.groups$;
    refreshTrigger.subscribe(() => {
      this.entityService.getEntities(this.entityIds$.value).then(entities => this.entities$.next(entities.map(e => this.filterEntity(e)).filter(e => e)));
    });
    let headerRefreshTrigger = merge( this.identityService.groups$, this.entityService.unsavedEntities$ );
    headerRefreshTrigger.subscribe(() => {
      this.entityService.getEntityHeaders().then(headers => this.entityHeaders$.next(this.filterHeaders(headers)));
    });
  }

  addEntity(entity: Entity){
    if(!entity)
      return;
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
      this.entities$.next(entities.filter(e => e.id != id));
    }
    if (entIds.includes(id)) {
      this.entityIds$.next(entIds.filter(e => e != id));
    }
  }

  moveToFront(id: number){
    let entities = this.entities$.value;
    let entIds = this.entityIds$.value;
    if (entities.filter(e => e?.id == id).length > 0) {
      this.entities$.next(entities.filter(e => e.id != id).concat(entities.filter(e => e.id == id)));
    }
    if (entIds.includes(id)) {
      this.entityIds$.next(entIds.filter(e => e != id).concat(entIds.filter(e => e == id)));
    }
  }

  filterHeaders(headers: EntityHeader[]): EntityHeader[] {
    let groups = this.identityService.groups$.getValue()?.map(g => g.id);
    let identity = this.identityService.identity$.getValue().id;
    return headers.filter(h => h.permissions.author.id == identity || [...h.permissions.perms.map(p => p.grantee.id), ...h.permissions.revealed.map(r => r.group.id)].filter(x => groups.includes(x)).length > 0);
  }

  filterEntity(entity: Entity): Entity {
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
