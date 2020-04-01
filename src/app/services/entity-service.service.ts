import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Entity } from '../interfaces/entity';
import { CHARACTERS } from '../mock-data/mock-character';
import { IdentityService } from './identity.service';
import { Detail } from '../interfaces/detail';
import { EntityHeader } from '../interfaces/entity-header';

@Injectable({
  providedIn: 'root'
})
export class EntityService {
  getEntities(ids: number[]): Observable<Entity[]>{
    let entities: Entity[] = [];
    ids.forEach(id => this.getEntity(id).subscribe(e => {
      if(e){
        entities.push(e);
      }
    }));
    return of(entities);
  }

  constructor(private identityService: IdentityService) { }

  getEntityHeaders(): Observable<EntityHeader[]>{
    let groups = this.identityService.groups$.getValue();
    let entities: EntityHeader[] = CHARACTERS.filter(c => 
      (groups.includes(1)
      || c.revealed?.filter(r => groups.includes(r.destinationId)).length > 0
      )).map(char => <EntityHeader>{
      id: char.id,
      name: char.name,
      revealed: char.revealed
    })

    return of(entities);
  }

  getEntity(id: number): Observable<Entity>{
    let identity = this.identityService.identity$.getValue();
    let groups = this.identityService.groups$.getValue();
    let filteredEntities = CHARACTERS.filter(c=>(
      c.id == id
      && (groups.includes(1) || 
        c.revealed?.filter(r => groups.includes(r.destinationId)).length>0)
    )).map(char => <Entity>{
      id: char.id,
        name: char.name,
        properties: char.properties.filter(p => 
          (groups.includes(1) // I am the runner
            || p.detail.author == identity.id // I am the author
            || p.detail.revealed.filter(r => groups.includes(r.destinationId)).length > 0)), // The information has been revealed to me
        details: char.details.filter(det =>
          (groups.includes(1) // I am the runner
            || det.author == identity.id // I am the author
            || det.revealed.filter(r => groups.includes(r.destinationId)).length > 0)) // The information has been revealed to me
      });
    let entity: Entity = null;
    if(filteredEntities.length==1){
      entity = filteredEntities[0];
    }
    return of(entity);
  }

  getCharacters(): Observable<Entity[]>{
    let identity = this.identityService.identity$.getValue();
    let groups = this.identityService.groups$.getValue();
    let Characters: Entity[] = CHARACTERS.map(char =>  <Entity>{
        id: char.id,
        name: char.name,
        properties: char.properties.filter(p => 
          (groups.includes(1) // I am the runner
            || p.detail.author == identity.id // I am the author
            || p.detail.revealed.filter(r => groups.includes(r.destinationId)).length > 0)), // The information has been revealed to me
        details: char.details.filter(det =>
          (groups.includes(1) // I am the runner
            || det.author == identity.id // I am the author
            || det.revealed.filter(r => groups.includes(r.destinationId)).length > 0)) // The information has been revealed to me
      });
    return of(Characters);
  }
}
