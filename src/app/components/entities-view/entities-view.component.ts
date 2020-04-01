import { Component, OnInit } from '@angular/core';
import { merge } from 'rxjs';
import { IdentityService } from 'src/app/services/identity.service';
import { Entity } from 'src/app/interfaces/entity';
import { ActiveEntitiesService } from 'src/app/services/active-entities.service';

@Component({
  selector: 'app-entities-view',
  templateUrl: './entities-view.component.html',
  styleUrls: ['./entities-view.component.css']
})
export class EntitiesViewComponent implements OnInit {

  entities: Entity[];

  constructor(private activeEntities: ActiveEntitiesService) { }

  ngOnInit(): void {
    this.activeEntities.entities$.subscribe(e => this.entities = e);
  }

  close(id: number){
    this.activeEntities.removeEntity(id);
  }

}
