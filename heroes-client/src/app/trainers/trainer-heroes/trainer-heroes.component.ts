import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { Hero } from 'src/app/_models/hero';
import { TrainerMember } from 'src/app/_models/trainerMember';
import { TrainerService } from 'src/app/_services/trainer.service';
import { AccountService } from '../../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { JwtHelperService } from '@auth0/angular-jwt';

const helper = new JwtHelperService();


@Component({
  selector: 'app-trainer-heroes',
  templateUrl: './trainer-heroes.component.html',
  styleUrls: ['./trainer-heroes.component.css'],
})
export class TrainerHeroesComponent implements OnInit {
  trainer: TrainerMember;
  heroes: Hero[];

  constructor(
    private accountService: AccountService,
    private trainerService: TrainerService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router
  ) {
    let id = Number.parseInt(this.route.snapshot.paramMap.get('id'));
    this.accountService.currentUser$.pipe(take(1)).subscribe((trainer) => {
      let token = helper.decodeToken(trainer.token);
      let trainerId = Number.parseInt(token.nameid);
      if (id !== trainerId) {
        this.toastr.error('Incorrect Id number');
        this.router.navigateByUrl('/not-found');
      }
    });
  }

  ngOnInit(): void {
    this.loadtrainerHeroes();
  }

  loadtrainerHeroes() {
    this.trainerService
      .getTrainerHeroes()
      .subscribe((trainer: TrainerMember) => {
        this.trainer = trainer;
        this.heroes = trainer.heroes;
      });
  }
}
