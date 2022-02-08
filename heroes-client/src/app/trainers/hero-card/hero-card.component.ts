import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AbilityType, ColorName, Hero } from 'src/app/_models/hero';
import { HeroService } from '../../_services/hero.service';

@Component({
  selector: 'app-hero-card',
  templateUrl: './hero-card.component.html',
  styleUrls: ['./hero-card.component.css'],
})
export class HeroCardComponent implements OnInit {
  @Input() hero: Hero;

  constructor(
    private heroService: HeroService,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
  }

  public getAbilityTypeName(type: number) {
    return AbilityType[type];
  }

  public getSuitColor(color: number) {
    return ColorName[color];
  }

  trainHero(heroId: string) {
    this.heroService
      .trainHero(heroId)
      .subscribe((response) => {
        if (response.status === 204) {
          this.toastr.error("Hero cannot train more than 5 times a day")
        }
        else {
        this.toastr.success(`${this.hero.name} trained successfully!`);
        }
        this.loadHero();
      });
  }

  loadHero() {
    this.heroService.getHero(this.hero.id).subscribe((hero) => {
      this.hero = hero;
    });
  }
}
