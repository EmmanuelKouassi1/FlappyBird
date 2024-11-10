import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Score } from '../models/score';
import { lastValueFrom } from 'rxjs';

const domain: string = "https://localhost:7197/";

@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  constructor(private http: HttpClient) { }

  private getHttpOptions() {
    let token = localStorage.getItem("token");
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      })
    };
  }

  // Get the current user's scores
  getMyScores(): Promise<Score[]> {
    return lastValueFrom(this.http.get<Score[]>(domain + "api/Scores", this.getHttpOptions()));
  }

  // Get public scores
  getPublicScores(): Promise<Score[]> {
    return lastValueFrom(this.http.get<Score[]>(domain + "api/Scores/GetPublicScores"));
  }

  // Change visibility of a score
  changeScoreVisibility(id: number): Promise<Score> {
    return lastValueFrom(this.http.put<Score>(domain + "api/Scores/ChangeScoreVisibility/" + id, null, this.getHttpOptions()));
  }

  // Post a new score
  postScore(score: Score): Promise<Score> {
    return lastValueFrom(this.http.post<Score>(domain + "api/Scores/PostScore", score, this.getHttpOptions()));
  }
}
