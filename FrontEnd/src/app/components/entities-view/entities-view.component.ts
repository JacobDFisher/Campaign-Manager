import { Component, OnInit } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';
import { ActiveEntitiesService } from 'src/app/services/active-entities.service';
import { CdkDragEnd, CdkDragStart } from '@angular/cdk/drag-drop';
import { CardProps } from 'src/app/interfaces/position';
import { EntityService } from 'src/app/services/entity.service';
import { ResizedEvent } from 'angular-resize-event';

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
    return {x: 0, y: 0, width: 500, height: 300, editMode: id<-100, storedX: 0, storedY: 0, storedWidth: 500, storedHeight: 300};
  }

  updateId(event: {old: number, new: number}){
    this.cardProps[event.new] = this.cardProps[event.old];
    delete this.cardProps[event.old];
  }

  onResized(event: ResizedEvent){
    debugger;
    let newVars: [string, CardProps][] = Object.entries(this.cardProps).map(c => [c[0], <CardProps> {
      editMode: c[1].editMode,
      storedHeight: c[1].storedHeight,
      storedWidth: c[1].storedWidth,
      storedX: c[1].storedX,
      storedY: c[1].storedY,
      height: Math.min(event.newHeight, c[1].storedHeight),
      width: Math.min(event.newWidth, c[1].storedWidth),
      x: Math.max(0, c[1].storedX+Math.min(0, event.newWidth-(c[1].storedX+c[1].storedWidth))),
      y: Math.max(0, c[1].storedY+Math.min(0, event.newHeight-(c[1].storedY+c[1].storedHeight)))
    }]);
    this.cardProps = Object.assign({}, ...Array.from(newVars, ([k,v]) => ({[k]: v})));
  }
}
