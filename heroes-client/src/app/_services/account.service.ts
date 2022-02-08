import { HttpClient } from '@angular/common/http';
import { Injectable, Output, EventEmitter } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthModel } from '../_models/authModel';
import { Trainer } from '../_models/trainer';
import { JwtHelperService } from '@auth0/angular-jwt';

const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<Trainer>(1);
  currentUser$ = this.currentUserSource.asObservable();
  @Output() getTrainerId = new EventEmitter();
  @Output() getTrainerName = new EventEmitter();

  constructor(private http: HttpClient) {}

  login(model: AuthModel) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: Trainer) => {
        if (response) {
          this.setCurrentUser(response);
          this.setTrainerDetails(response);
        }
        return response;
      })
    );
  }

  register(model: AuthModel) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((response: Trainer) => {
        if (response) {
          this.setCurrentUser(response);
          this.setTrainerDetails(response);
        }
        return response;
      })
    );
  }

  setCurrentUser(trainer: Trainer) {
    localStorage.setItem('trainer', JSON.stringify(trainer));
    this.currentUserSource.next(trainer);
  }

  setTrainerDetails(trainer: Trainer) {
    let token = helper.decodeToken(trainer.token);
    let trainerId = Number.parseInt(token.nameid);
    let trainerName = token.unique_name;
    this.getTrainerId.emit(trainerId);
    this.getTrainerName.emit(trainerName);
  }

  logout() {
    localStorage.removeItem('trainer');
    this.currentUserSource.next(null);
  }
}
