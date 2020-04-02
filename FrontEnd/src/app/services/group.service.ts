import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GROUPS } from '../mock-data/mock-group';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Group } from '../interfaces/group';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private httpClient: HttpClient) { }

  endpoint = environment.endpoint+'api/group/'

  getGroups(groups: number[]): Observable<Group[]>{
    let params = new HttpParams();
    let count = 0;
    groups.forEach(n => params = params.set(`ids[${count++}]`, `${n}`));
    let options = {params: params}
    return this.httpClient.get<Group[]>(this.endpoint, options);
    // let retGroups = [0];
    // while(groups && groups.length > 0){
    //   retGroups = retGroups.concat(groups);
    //   groups = [].concat(...GROUPS.filter(g => groups.includes(g.id)).map(g => g.memberOf).filter(g => g!=null));
    // }
    // return of(retGroups);
  }
}
