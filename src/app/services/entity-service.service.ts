import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Entity } from '../interfaces/entity';
import { CHARACTERS } from '../mock-data/mock-character';
import { IdentityService } from './identity.service';
import { Detail } from '../interfaces/detail';

@Injectable({
  providedIn: 'root'
})
export class EntityService {

  constructor(private identityService: IdentityService) { }

  getCharacters(): Observable<Entity[]>{
    let identity = this.identityService.identity$.getValue();
    let groups = this.identityService.groups$.getValue();
    let Characters: Entity[] = CHARACTERS.map(char =>  <Entity>{
        id: char.id,
        properties: char.properties.filter(p => 
          (groups.includes("Runner") // I am the runner
            || p.detail.author == identity.name // I am the author
            || p.detail.revealed.filter(r => groups.includes(r.destination)).length > 0)), // The information has been revealed to me
        details: char.details.filter(det =>
          (groups.includes("Runner") // I am the runner
            || det.author == identity.name // I am the author
            || det.revealed.filter(r => groups.includes(r.destination)).length > 0)) // The information has been revealed to me
      });
    return of(Characters);
  }
}
