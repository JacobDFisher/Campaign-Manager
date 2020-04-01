import { Component, OnInit, Input } from '@angular/core';
import { Detail } from 'src/app/interfaces/detail';

@Component({
  selector: 'app-detail-view',
  templateUrl: './detail-view.component.html',
  styleUrls: ['./detail-view.component.css']
})
export class DetailViewComponent implements OnInit {

  @Input() detail: Detail
  constructor() { }

  ngOnInit(): void {
  }

}
