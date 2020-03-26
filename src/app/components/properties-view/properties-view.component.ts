import { Component, OnInit, Input } from '@angular/core';
import { Property } from 'src/app/interfaces/property';

@Component({
  selector: 'app-properties-view',
  templateUrl: './properties-view.component.html',
  styleUrls: ['./properties-view.component.css']
})
export class PropertiesViewComponent implements OnInit {

  @Input() properties: Property[];

  constructor() { }

  ngOnInit(): void {
  }

}
