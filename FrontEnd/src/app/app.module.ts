import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { IdentitySelectorComponent } from './components/identity-selector/identity-selector.component';
import { DetailViewComponent } from './components/detail-view/detail-view.component';
import { EntityViewComponent } from './components/entity-view/entity-view.component';
import { PropertiesViewComponent } from './components/properties-view/properties-view.component';
import { PropertyViewComponent } from './components/property-view/property-view.component';
import { DetailsViewComponent } from './components/details-view/details-view.component';
import { EntitiesViewComponent } from './components/entities-view/entities-view.component';
import { EntityListComponent } from './components/entity-list/entity-list.component';
import { EntityCardComponent } from './components/entity-card/entity-card.component';
import { EntityEditComponent } from './components/entity-edit/entity-edit.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { DragDropModule } from '@angular/cdk/drag-drop';
import {MatDividerModule} from '@angular/material/divider';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {MatSidenavModule} from '@angular/material/sidenav';
import { AngularResizedEventModule } from 'angular-resize-event';

@NgModule({
  declarations: [
    AppComponent,
    IdentitySelectorComponent,
    DetailViewComponent,
    EntityViewComponent,
    PropertiesViewComponent,
    PropertyViewComponent,
    DetailsViewComponent,
    EntitiesViewComponent,
    EntityListComponent,
    EntityCardComponent,
    EntityEditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCardModule,
    DragDropModule,
    MatDividerModule,
    ScrollingModule,
    MatSidenavModule,
    AngularResizedEventModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
