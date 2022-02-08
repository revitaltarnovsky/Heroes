import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { take } from 'rxjs/operators';
import { Trainer } from './_models/trainer';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {

  constructor(private accountService: AccountService) {}
  
  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const trainer: Trainer = JSON.parse(localStorage.getItem('trainer'));
    this.accountService.setCurrentUser(trainer);
  }
}
