import { Injectable } from '@angular/core';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { Identity } from '../interfaces/identity';
import { IDENTITIES } from '../mock-data/mock-identity';
import { GroupService } from './group.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Group } from '../interfaces/group';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  constructor(private groupService: GroupService, private httpClient: HttpClient) {
    this.identity$ = new BehaviorSubject(null);
    this.groups$ = new BehaviorSubject(null);
    this.setIdentity(1);
  }

  endpoint = environment.endpoint+'api/identity/'

  identity$: BehaviorSubject<Identity>;
  groups$: BehaviorSubject<Group[]>;

  setIdentity(id: number): void{
    this.httpClient.get<Identity>(this.endpoint+id).subscribe(newIdentity =>{
    this.identity$.next(newIdentity); 
    this.groupService.getGroups(newIdentity.groups.map(g => g.id)).subscribe(
      groups => this.groups$.next(groups)
    );
  });
  }

  getIdentities(): Observable<Identity[]>{
    return this.httpClient.get<Identity[]>(this.endpoint);
  }
}
