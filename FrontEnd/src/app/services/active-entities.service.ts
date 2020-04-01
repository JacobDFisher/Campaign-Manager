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
      this.entityService.getEntities(this.entityIds$.value).subscribe(chars => this.entities$.next(chars));
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
      this.entities$.next(entities.concat(entity));
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
}
