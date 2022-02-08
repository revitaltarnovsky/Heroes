import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Hero } from '../_models/hero';

@Injectable()
export class HeroService {
  baseurl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getHeroes() {
    return this.http.get<Hero[]>(this.baseurl + 'heroes');
  }

  getHero(id: string) {
    return this.http.get<Hero>(this.baseurl + 'heroes/' + id);
  }

  trainHero(id: string) {
    return this.http.patch<any>(
      this.baseurl + 'heroes/' + id, {}, {observe: "response"}
    );
  }

  addHero(model: Hero) {
    return this.http.post<Hero>(
      this.baseurl + 'heroes',
      model
    );
  }
}
