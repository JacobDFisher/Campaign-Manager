import { Component, OnInit } from '@angular/core';
import { IdentityService } from 'src/app/services/identity.service';
import { Identity } from 'src/app/interfaces/identity';

@Component({
  selector: 'app-identity-selector',
  templateUrl: './identity-selector.component.html',
  styleUrls: ['./identity-selector.component.css']
})
export class IdentitySelectorComponent implements OnInit {

  identities: Identity[];
  identity: Identity;
  groups: string[];

  constructor(public identityService: IdentityService) { }

  ngOnInit(): void {
    this.identityService.identities$.subscribe(ids => {
      this.identities = ids
    });
    this.identityService.identity$.subscribe(id => {
      this.identity = id;
    });
    this.identityService.groups$.subscribe(groups => {
      this.groups = groups;
    });
  }

  selectIdentity(idName: string): void{
    this.identityService.setIdentity(idName);
  }
}
