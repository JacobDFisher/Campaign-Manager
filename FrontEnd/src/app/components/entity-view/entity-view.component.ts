import { Component, OnInit, Input } from '@angular/core';
import { Entity } from 'src/app/interfaces/entity';
import { EntityService } from 'src/app/services/entity-service.service';
import { IdentityService } from 'src/app/services/identity.service';

@Component({
  selector: 'app-entity-view',
  templateUrl: './entity-view.component.html',
  styleUrls: ['./entity-view.component.css']
})
export class EntityViewComponent implements OnInit {
  @Input() entity: Entity;

  constructor() { }

  ngOnInit(): void {
  }

}
