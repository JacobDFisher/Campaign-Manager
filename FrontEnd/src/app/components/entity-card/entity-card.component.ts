import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';
import { MyPosition, MySize } from 'src/app/interfaces/position';
import { CdkDragEnd } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-entity-card',
  templateUrl: './entity-card.component.html',
  styleUrls: ['./entity-card.component.css']
})
export class EntityCardComponent implements OnInit {
  @Input() entity: Entity;
  @Input() position: MyPosition;
  @Input() size: MySize;
  @Output() close = new EventEmitter();
  @Output() bringToTop = new EventEmitter<number>();
  @Output() move = new EventEmitter<{id: number, pos: MyPosition}>();
  @Output() resize = new EventEmitter<{id: number, size: MySize}>();
  editMode: boolean;

  constructor() {  }

  ngOnInit(): void {
    this.editMode = false;
  }

  toggleEditMode(){
    this.editMode = !this.editMode;
  }

  endMove(event: CdkDragEnd){
    this.move.emit({id: this.entity.id, pos: event.source.getFreeDragPosition()});
  }

  startMove(event: MouseEvent){
    this.bringToTop.emit(this.entity.id);
  }
}
