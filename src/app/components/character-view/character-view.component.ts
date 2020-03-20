import { Component, OnInit } from '@angular/core';
import { EntityService } from 'src/app/services/entity-service.service';
import { IdentityService } from 'src/app/services/identity.service';
import { Entity } from 'src/app/interfaces/entity';
import { merge } from 'rxjs';

@Component({
  selector: 'app-character-view',
  templateUrl: './character-view.component.html',
  styleUrls: ['./character-view.component.css']
})
export class CharacterViewComponent implements OnInit {

  constructor(public characterService: EntityService, private identityService: IdentityService) { }

  characters: Entity[];

  ngOnInit(): void {
    merge(this.identityService.identity$, this.identityService.groups$).subscribe(() => {
      this.characterService.getCharacters().subscribe(chars => this.characters = chars);
    })
  }

}
