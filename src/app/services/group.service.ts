import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GROUPS } from '../mock-data/mock-group';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor() { }

  getGroups(groups: string[]): Observable<string[]>{
    let retGroups = ['All'];
    while(groups && groups.length > 0){
      retGroups = retGroups.concat(groups);
      groups = [].concat(...GROUPS.filter(g => groups.includes(g.name)).map(g => g.memberOf).filter(g => g!=null));
    }
    return of(retGroups);
  }
}
