import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';
import { Detail } from 'src/app/interfaces/detail';

@Component({
  selector: 'app-entity-edit',
  templateUrl: './entity-edit.component.html',
  styleUrls: ['./entity-edit.component.css']
})
export class EntityEditComponent implements OnInit {
  @Input() entity: Entity;
  @Output() update: EventEmitter<Entity> = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  updateDetails(details: Detail[]){
    this.entity.details = details;
    this.updateEntity();
  }

  updateEntity(){
    this.update.emit(this.entity);
  }
}
