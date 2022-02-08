import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainerHeroesComponent } from './trainer-heroes/trainer-heroes.component';
import { HeroCardComponent } from './hero-card/hero-card.component';
import { TrainersRoutingModule } from './trainers-routing.module';
import { TrainerService } from '../_services/trainer.service';
import { SharedModule } from '../_modules/shared.module';




@NgModule({
  declarations: [
    TrainerHeroesComponent,
    HeroCardComponent
  ],
  imports: [
    CommonModule,
    TrainersRoutingModule,
    SharedModule
  ],
  providers: [TrainerService]
})
export class TrainersModule { }
