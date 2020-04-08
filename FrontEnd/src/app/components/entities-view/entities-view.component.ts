import { Component, OnInit } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';
import { ActiveEntitiesService } from 'src/app/services/active-entities.service';
import { CdkDragEnd, CdkDragStart } from '@angular/cdk/drag-drop';
import { MyPosition, MySize } from 'src/app/interfaces/position';

@Component({
  selector: 'app-entities-view',
  templateUrl: './entities-view.component.html',
  styleUrls: ['./entities-view.component.css']
})
export class EntitiesViewComponent implements OnInit {

  entities: Entity[];
  positions: {[id: number]: MyPosition};
  sizes: {[id: number]: MySize};

  constructor(private activeEntities: ActiveEntitiesService) { }

  ngOnInit(): void {
    this.positions = {};
    this.sizes = {};
    this.activeEntities.entities$.subscribe(e => {this.entities = e;});
  }

  close(id: number){
    this.positions[id] = {x: 0, y: 0};
    this.activeEntities.removeEntity(id);
  }

  updatePosition(event: {id: number, pos: MyPosition}){
    this.positions[event.id] = event.pos;
  }

  updateSize(event:{id: number, size: MySize}){
    this.sizes[event.id] = event.size;
  }

  bringToFront(event: number){
    this.activeEntities.moveToFront(event);
  }
}
