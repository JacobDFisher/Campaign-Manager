import { Injectable } from '@angular/core';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { Identity } from '../interfaces/identity';
import { IDENTITIES } from '../mock-data/mock-identity';
import { GroupService } from './group.service';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  constructor(private groupService: GroupService) {
    this.identity$ = new BehaviorSubject(IDENTITIES[0]);
    this.identities$ = new BehaviorSubject(IDENTITIES);
    groupService.getGroups(IDENTITIES[0].groups).subscribe(grps => this.groups$ = new BehaviorSubject(grps));
  }

  identities$: BehaviorSubject<Identity[]>;
  identity$: BehaviorSubject<Identity>;
  groups$: BehaviorSubject<string[]>;

  setIdentity(name: string): void{
    let newIdentity = IDENTITIES.find(e => e.name==name);
    this.identity$.next(newIdentity); 
    this.groupService.getGroups(newIdentity.groups).subscribe(
      groups => this.groups$.next(groups)
    );
  }

  getIdentities(): Observable<Identity[]>{
    return of(IDENTITIES);
  }
}
