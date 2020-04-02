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
    EntityListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
