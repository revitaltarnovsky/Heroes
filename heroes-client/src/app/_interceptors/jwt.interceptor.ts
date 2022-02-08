import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Trainer } from '../_models/trainer';
import { AccountService } from '../_services/account.service';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    let currentTrainer: Trainer;

    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((trainer) => (currentTrainer = trainer));
    if (currentTrainer) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentTrainer.token}`,
        },
      });
    }
    return next.handle(request);
  }
}
