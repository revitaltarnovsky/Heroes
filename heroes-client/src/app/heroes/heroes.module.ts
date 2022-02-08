import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroesListComponent } from './heroes-list/heroes-list.component';
import { AddHeroComponent } from './add-hero/add-hero.component';
import { HeroesRoutingModule } from './heroes-routing.module';
import { SharedModule } from '../_modules/shared.module';


@NgModule({
  declarations: [
    HeroesListComponent,
    AddHeroComponent,
  ],
  imports: [
    CommonModule,
    HeroesRoutingModule,
    SharedModule
  ]
})
export class HeroesModule { }
