import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Detail } from 'src/app/interfaces/detail';
import { IdentityService } from 'src/app/services/identity.service';
import { PermissionHolder } from 'src/app/interfaces/permissionHolder';
import { Identity } from 'src/app/interfaces/identity';

@Component({
  selector: 'app-details-view',
  templateUrl: './details-view.component.html',
  styleUrls: ['./details-view.component.css']
})
export class DetailsViewComponent implements OnInit {

  @Input() details: Detail[];
  @Input() editMode: boolean;
  @Output() update: EventEmitter<Detail[]> = new EventEmitter();
  constructor(private identityService: IdentityService) { }
  count = 0;

  ngOnInit(): void {
  }

  addDetail(){
    this.details = [...this.details, {
      permissions: <PermissionHolder> {
      author: <Identity> {
        id: this.identityService.identity$.getValue().id
      },
      perms: [],
      revealed: []
    },
    description: `Detail ${this.count++}`}]
    this.update.emit(this.details);
  }
}
