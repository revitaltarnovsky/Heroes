import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Hero } from '../_models/hero';
import { TrainerMember } from '../_models/trainerMember';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TrainerService {
  baseurl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTrainerHeroes() {
    return this.http.get<TrainerMember>(
      this.baseurl + 'trainers/trainer'
    );
  }
}
