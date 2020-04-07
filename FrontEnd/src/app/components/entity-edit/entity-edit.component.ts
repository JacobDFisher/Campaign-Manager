import { Component, OnInit, Input } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';

@Component({
  selector: 'app-entity-edit',
  templateUrl: './entity-edit.component.html',
  styleUrls: ['./entity-edit.component.css']
})
export class EntityEditComponent implements OnInit {
  @Input() entity: Entity;
  constructor() { }

  ngOnInit(): void {
  }

}
