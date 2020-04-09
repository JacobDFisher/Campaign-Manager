import { Component, OnInit } from '@angular/core';
import { EntityHeader } from 'src/app/interfaces/entity-header';
import { EntityService } from 'src/app/services/entity.service';
import { IdentityService } from 'src/app/services/identity.service';
import { merge } from 'rxjs';
import { ActiveEntitiesService } from 'src/app/services/active-entities.service';

@Component({
  selector: 'app-entity-list',
  templateUrl: './entity-list.component.html',
  styleUrls: ['./entity-list.component.css']
})
export class EntityListComponent implements OnInit {

  entities: EntityHeader[];

  constructor(public entityService: EntityService, private identityService: IdentityService, private activeEntitiesService: ActiveEntitiesService) { }

  ngOnInit(): void {
    //let refreshTrigger = merge(this.identityService.identity$, this.identityService.groups$);
    let refreshTrigger = this.activeEntitiesService.entityHeaders$;
    refreshTrigger.subscribe(headers => {
      this.entities = headers;
    });
  }

  selectEntity(id: number){
    this.entityService.getEntity(id).subscribe(ent => this.activeEntitiesService.addEntity(ent));
  }

  newEntity(){
    this.activeEntitiesService.addEntity(this.entityService.newEntity());
  }

}
