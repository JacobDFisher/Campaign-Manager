import { Component, OnInit, Input } from '@angular/core';
import { Detail } from 'src/app/interfaces/detail';

@Component({
  selector: 'app-details-view',
  templateUrl: './details-view.component.html',
  styleUrls: ['./details-view.component.css']
})
export class DetailsViewComponent implements OnInit {

  @Input() details: Detail[];
  constructor() { }

  ngOnInit(): void {
  }

}
