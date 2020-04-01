import { Component, OnInit, Input } from '@angular/core';
import { Property } from 'src/app/interfaces/property';

@Component({
  selector: 'app-property-view',
  templateUrl: './property-view.component.html',
  styleUrls: ['./property-view.component.css']
})
export class PropertyViewComponent implements OnInit {

  @Input() property: Property;
  constructor() { }

  ngOnInit(): void {
  }

}
