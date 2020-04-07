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
    merge(this.identityService.identity$, this.identityService.groups$).subscribe(() => {
      this.entityService.getEntityHeaders().subscribe(chars => this.entities = chars);
    });
  }

  selectEntity(id: number){
    this.entityService.getEntity(id).subscribe(ent => this.activeEntitiesService.addEntity(ent));
  }

}
