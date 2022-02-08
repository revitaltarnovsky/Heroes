import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { take } from 'rxjs/operators';

const helper = new JwtHelperService();

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  trainerId: number;
  trainerName: string;

  constructor(public accountService: AccountService, private router: Router) {
    this.accountService.getTrainerId.subscribe(
      (trainerId) => (this.trainerId = trainerId)
    );
    this.accountService.getTrainerName.subscribe(
      (trainerName) => (this.trainerName = trainerName)
    );
  }

  ngOnInit(): void {
    this.loadTrainerDetails();
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  loadTrainerDetails() {
    let trainer = JSON.parse(localStorage.getItem('trainer'));
    if (trainer !== null) {
      this.accountService.currentUser$.pipe(take(1)).subscribe((trainer) => {
        let token = helper.decodeToken(trainer.token);
        this.trainerId = Number.parseInt(token.nameid);
        this.trainerName = token.unique_name;
      });
    }
  }
}
