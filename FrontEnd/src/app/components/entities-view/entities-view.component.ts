import { Component, OnInit } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';
import { ActiveEntitiesService } from 'src/app/services/active-entities.service';
import { CdkDragEnd, CdkDragStart } from '@angular/cdk/drag-drop';
import { CardProps } from 'src/app/interfaces/position';
import { EntityService } from 'src/app/services/entity.service';

@Component({
  selector: 'app-entities-view',
  templateUrl: './entities-view.component.html',
  styleUrls: ['./entities-view.component.css']
})
export class EntitiesViewComponent implements OnInit {

  entities: Entity[];
  cardProps: {[id: number]: CardProps};

  constructor(private activeEntities: ActiveEntitiesService) { }

  ngOnInit(): void {
    this.cardProps = {};
    this.activeEntities.entities$.subscribe(e => {this.entities = e;});
  }

  close(id: number){
    delete this.cardProps[id];
    this.activeEntities.removeEntity(id);
  }

  updateProps(event: {id: number, props: CardProps}){
    this.cardProps[event.id] = event.props;
  }

  bringToFront(event: number){
    this.activeEntities.moveToFront(event);
  }

  defaultProps(id: number): CardProps{
    return {x: 0, y: 0, width: 500, height: 300, editMode: id<-100};
  }

  updateId(event: {old: number, new: number}){
    this.cardProps[event.new] = this.cardProps[event.old];
    delete this.cardProps[event.old];
  }
}
