import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';
import { CardProps } from 'src/app/interfaces/position';
import { CdkDragEnd } from '@angular/cdk/drag-drop';
import { EntityService } from 'src/app/services/entity.service';

@Component({
  selector: 'app-entity-card',
  templateUrl: './entity-card.component.html',
  styleUrls: ['./entity-card.component.css']
})
export class EntityCardComponent implements OnInit {
  @Input() entity: Entity;
  @Input() properties: CardProps;
  @Output() close = new EventEmitter();
  @Output() bringToTop = new EventEmitter<number>();
  @Output() propChange = new EventEmitter<{id: number, props: CardProps}>();
  @Output() idChange = new EventEmitter<{old: number, new: number}>();

  constructor(private entityService: EntityService) {  }

  ngOnInit(): void {
  }

  updateEntity(entity: Entity){
    this.entity = entity;
    this.tempStore();
  }

  private updateId(id: number){
    this.idChange.emit({old: this.entity.id, new: id});
    this.entity.id = id;
  }

  tempStore(){
    this.entityService.storeEntity(this.entity);
  }

  saveEntity(){
    this.entityService.storeEntity(this.entity);
    this.entityService.saveEntity(this.entity.id).then(
      id => {
        if(id != this.entity.id)
          this.updateId(id)
      }
    );
  }

  toggleEditMode(){
    if(this.properties.editMode){
      this.saveEntity();
    }
    this.properties.editMode = !this.properties.editMode;
    this.notifyProps();
  }

  endMove(event: CdkDragEnd){
    let newPos = event.source.getFreeDragPosition();
    this.properties.x = newPos.x;
    this.properties.y = newPos.y;
    this.notifyProps();
  }

  startMove(event: MouseEvent){
    this.bringToTop.emit(this.entity.id);
  }

  private notifyProps(){
    this.propChange.emit({id: this.entity.id, props: this.properties});
  }
}
