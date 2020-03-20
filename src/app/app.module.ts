import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { IdentitySelectorComponent } from './components/identity-selector/identity-selector.component';
import { CharacterViewComponent } from './components/character-view/character-view.component';
import { DetailViewComponent } from './components/detail-view/detail-view.component';

@NgModule({
  declarations: [
    AppComponent,
    IdentitySelectorComponent,
    CharacterViewComponent,
    DetailViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
