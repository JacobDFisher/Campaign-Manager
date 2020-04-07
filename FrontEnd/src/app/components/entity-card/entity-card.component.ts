import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';

@Component({
  selector: 'app-entity-card',
  templateUrl: './entity-card.component.html',
  styleUrls: ['./entity-card.component.css']
})
export class EntityCardComponent implements OnInit {
  @Input() entity: Entity;
  @Output() close = new EventEmitter();
  editMode: boolean;

  constructor() {  }

  ngOnInit(): void {
    this.editMode = false;
  }

  toggleEditMode(){
    this.editMode = !this.editMode;
  }
}
