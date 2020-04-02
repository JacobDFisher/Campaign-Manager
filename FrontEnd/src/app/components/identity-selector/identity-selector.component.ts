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
    this.identityService.getIdentities().subscribe(ids => {
      this.identities = ids
    });
    this.identityService.identity$.subscribe(id => {
      this.identity = id;
    });
    this.identityService.groups$.subscribe(groups => {
      this.groups = groups?.map(g => g.name);
    });
  }

  selectIdentity(id: number): void{
    this.identityService.setIdentity(id);
  }
}
