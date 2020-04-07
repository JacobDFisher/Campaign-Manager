import { Component, OnInit } from '@angular/core';
import { merge } from 'rxjs';
import { IdentityService } from 'src/app/services/identity.service';
import { Entity } from 'src/app/interfaces/entity';
import { ActiveEntitiesService } from 'src/app/services/active-entities.service';
import { CdkDragEnd } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-entities-view',
  templateUrl: './entities-view.component.html',
  styleUrls: ['./entities-view.component.css']
})
export class EntitiesViewComponent implements OnInit {

  entities: Entity[];
  positions: {[id: number]: {x: number, y: number}};

  constructor(private activeEntities: ActiveEntitiesService) { }

  ngOnInit(): void {
    this.positions = {};
    this.activeEntities.entities$.subscribe(e => {this.entities = e;});
  }

  close(id: number){
    this.positions[id] = {x: 0, y: 0};
    this.activeEntities.removeEntity(id);
  }

  logEvent(event: CdkDragEnd){
    console.log(event.source.getFreeDragPosition());
    console.log(event.source.data);
    this.positions[event.source.data] = event.source.getFreeDragPosition();
  }
}
