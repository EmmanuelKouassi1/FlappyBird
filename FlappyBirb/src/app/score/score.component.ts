import { Component } from '@angular/core';
import { Score } from '../models/score';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { Round00Pipe } from '../pipes/round-00.pipe';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
const domain : string="https://localhost:7197/"
@Component({
  selector: 'app-score',
  standalone: true,
  imports: [MaterialModule, CommonModule, Round00Pipe],
  templateUrl: './score.component.html',
  styleUrl: './score.component.css'
})
export class ScoreComponent {

  myScores : Score[] = [];
  publicScores : Score[] = [];
  userIsConnected : boolean = false;

  constructor(public route : Router, private http: HttpClient) { }

  async ngOnInit() {
    let token = localStorage.getItem("token");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer '+ token
      })
    };

    this.userIsConnected = localStorage.getItem("token") != null;
  //requete pour remplir myScores et publicScores
  if(this.userIsConnected){
    this.myScores  = await lastValueFrom(this.http.get<Score[]>(domain + "api/Scores",httpOptions));
    console.log(this.myScores);

  }
 

  }

  async changeScoreVisibility(score : Score){
  if(!score.visibilite){
    score.visibilite = true
  }
  score.visibilite = false

  }

}
