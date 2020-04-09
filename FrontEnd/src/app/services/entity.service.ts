import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { Entity } from '../interfaces/entity';
import { IdentityService } from './identity.service';
import { EntityHeader } from '../interfaces/entity-header';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PermissionHolder } from '../interfaces/permissionHolder';
import { Identity } from '../interfaces/identity';

@Injectable({
  providedIn: 'root'
})
export class EntityService {
  constructor(private identityService: IdentityService,
    private httpClient: HttpClient) { }

  private endpoint = environment.endpoint+'api/entity/';
  unsavedEntities$: BehaviorSubject<Entity[]> = new BehaviorSubject([]);

  async getEntities(ids: number[]): Promise<Entity[]>{
    if(ids==null||ids.length==0){
      return [];
    }
    let unsavedEntities = this.unsavedEntities$.value;
    let getIds = ids.filter(id => unsavedEntities.filter(e => e.id == id).length == 0);
    let params = new HttpParams();
    let count = 0;
    getIds.forEach(n => params = params.set(`ids[${count++}]`, `${n}`));
    let options = {params: params};
    let entities = await this.httpClient.get<Entity[]>(this.endpoint, options).toPromise();
    let retEntities = [].concat(...ids.map(id => entities.filter(e => e.id == id).concat(unsavedEntities.filter(e => e.id == id))));
    return retEntities;
  }

  async getEntityHeaders(): Promise<EntityHeader[]>{
    let unsavedEntities = this.unsavedEntities$.value;
    let headers = await this.httpClient.get<EntityHeader[]>(this.endpoint+'header/').toPromise();
    return headers.filter(e => unsavedEntities.filter(u => u.id == e.id).length == 0).concat(unsavedEntities);
  }

  getEntity(id: number): Observable<Entity>{
    let filtered = this.unsavedEntities$.value.filter(e => e.id == id);
    if(filtered.length > 0)
      return of(filtered[0]);
    return this.httpClient.get<Entity>(this.endpoint+id);
  }

  newEntity() : Entity{
    let entity = <Entity> {
      id: Math.min(-100, ...this.unsavedEntities$.value.map(e => e.id))-1,
      name: "",
      details: [],
      properties: [],
      permissions: <PermissionHolder> {
        author: <Identity> {
          id: this.identityService.identity$.getValue().id
        },
        perms: [],
        revealed: []
      }
    }
    this.storeEntity(entity);
    return entity;
  }

  storeEntity(entity: Entity){
    let unsaved = this.unsavedEntities$.value;
    let index = unsaved.map(e => e.id).indexOf(entity.id);
    if(index > -1)
      this.unsavedEntities$.next([...unsaved.slice(0, index), entity, ...unsaved.slice(index+1)]);
    else
      this.unsavedEntities$.next([...unsaved, entity]);
  }

  unstoreEntity(id: number){
    this.unsavedEntities$.next(this.unsavedEntities$.value.filter(e => e.id != id));
  }

  async saveEntity(id: number): Promise<number>{
    let filtered = this.unsavedEntities$.value.filter(e => e.id == id);
    if(filtered.length == 0)
      return;
    if(id>=-100){
      this.putEntity(filtered[0]).subscribe(
        () => this.unstoreEntity(id),
        err => console.log(err)
      );
      return id;
    } else {
      let e = await this.postEntity(filtered[0]).toPromise();
      this.unstoreEntity(id);
      return e.id;
    }
  }

  private putEntity(entity: Entity): Observable<any>{
    return this.httpClient.put(this.endpoint, entity);
  }

  private postEntity(entity: Entity): Observable<Entity>{
    let entityCopy = <Entity> Object.assign({}, entity, {id: 0});
    return this.httpClient.post<Entity>(this.endpoint, entityCopy);
  }
}
