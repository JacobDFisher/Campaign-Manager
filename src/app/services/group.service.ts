import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GROUPS } from '../mock-data/mock-group';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor() { }

  getGroups(groups: number[]): Observable<number[]>{
    let retGroups = [0];
    while(groups && groups.length > 0){
      retGroups = retGroups.concat(groups);
      groups = [].concat(...GROUPS.filter(g => groups.includes(g.id)).map(g => g.memberOf).filter(g => g!=null));
    }
    return of(retGroups);
  }
}
