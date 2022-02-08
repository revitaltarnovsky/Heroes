import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AbilityType } from 'src/app/_models/hero';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HeroService } from 'src/app/_services/hero.service';
import { ColorName } from '../../_models/hero';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from '../../_services/account.service';
import { take } from 'rxjs/operators';

const helper = new JwtHelperService();

@Component({
  selector: 'app-add-hero',
  templateUrl: './add-hero.component.html',
  styleUrls: ['./add-hero.component.css'],
})
export class AddHeroComponent implements OnInit {
  addHeroForm: FormGroup;
  abilityTypes = Object.values(AbilityType).filter(
    (value) => typeof value === 'number'
  );
  colorNames = Object.values(ColorName).filter(
    (value) => typeof value === 'number'
  );
  trainerId: number;
  validationErrors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private heroService: HeroService,
    private router: Router,
    private toastr: ToastrService,
    private accountService: AccountService
  ) {
    const navigation = this.router.getCurrentNavigation();
    this.trainerId = navigation?.extras?.state?.trainerId;
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  public getAbilityTypeName(type: number) {
    return AbilityType[type];
  }

  public getColorName(color: number) {
    return ColorName[color];
  }

  initializeForm() {
    this.addHeroForm = this.fb.group({
      name: ['', [Validators.required,Validators.minLength(2), Validators.maxLength(8)]],
      ability: [null, Validators.required],
      suitColors: [null, Validators.required],
    });
  }

  addHero() {
    this.heroService.addHero(this.addHeroForm.value).subscribe(
      (response) => {
        this.toastr.success('Hero added successfully');
        this.accountService.currentUser$.pipe(take(1)).subscribe((trainer) => {
          let token = helper.decodeToken(trainer.token);
          this.trainerId = Number.parseInt(token.nameid);
        });
        this.router.navigateByUrl('/trainers/' + this.trainerId + '/heroes');
      },
      (error) => {
        this.validationErrors = error;
      }
    );
  }
}
