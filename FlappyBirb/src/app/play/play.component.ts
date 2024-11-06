import { Component, OnInit } from '@angular/core';
import { Game } from './gameLogic/game';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { Score } from '../models/score';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
const domain : string="https://localhost:7197/"
@Component({
  selector: 'app-play',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './play.component.html',
  styleUrl: './play.component.css'
})
export class PlayComponent implements OnInit{

  game : Game | null = null;
  scoreSent : boolean = false;

  constructor(public route : Router, private http: HttpClient) { }


  ngOnDestroy(): void {
    // Ceci est crotté mais ne le retirez pas sinon le jeu bug.
    location.reload();
  }

  ngOnInit() {
    this.game = new Game();
  }

  replay(){
    if(this.game == null) return;
    this.game.prepareGame();
    this.scoreSent = false;
  }

  async sendScore(): Promise<void>{
    if(this.scoreSent) return;
    this.scoreSent = true;
    let leScore: number = (JSON.parse(sessionStorage.getItem("score") !)); 
    let temps: number = (JSON.parse(sessionStorage.getItem("time")  !)); 
    let id: number = 0;
    let visibilité: boolean =true;
   let score = new Score(id,null,null,temps,leScore,visibilité)
   
    if (leScore ==null || temps == null) {
        console.error("score invalide");
        return;
    }
    let token = localStorage.getItem("token");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer '+ token
      })
    };


    let x = await lastValueFrom(this.http.post<Score>(domain + "api/Scores", score, httpOptions));
    console.log(x);

  }
    // ██ Appeler une requête pour envoyer le score du joueur ██
    // Le score est dans sessionStorage.getItem("score")
    // Le temps est dans sessionStorage.getItem("time")
    // La date sera choisie par le serveur


}
