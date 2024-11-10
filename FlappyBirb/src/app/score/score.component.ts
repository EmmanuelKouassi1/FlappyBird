import { Component } from '@angular/core';
import { Score } from '../models/score';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';
import { Round00Pipe } from '../pipes/round-00.pipe';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { ScoreService } from '../services/score.service';
const domain : string="https://localhost:7197/"
@Component({
  selector: 'app-score',
  standalone: true,
  imports: [MaterialModule, CommonModule, Round00Pipe],
  templateUrl: './score.component.html',
  styleUrl: './score.component.css'
})
export class ScoreComponent {

  myScores: Score[] = [];
  publicScores: Score[] = [];
  userIsConnected: boolean = false;

  constructor(
    public route: Router, 
    private scoreService: ScoreService // Inject the service
  ) { }

  async ngOnInit() {
    this.userIsConnected = localStorage.getItem("token") != null;

    if (this.userIsConnected) {
      this.myScores = await this.scoreService.getMyScores();
      console.log(this.myScores);
    }
  }

  async getPublicScores() {
    this.publicScores = await this.scoreService.getPublicScores();
    console.log(this.publicScores);
  }

  async changeScoreVisibilitxy(id: number): Promise<void> {
    const updatedScore = await this.scoreService.changeScoreVisibility(id);
    console.log(updatedScore);
  }

  async postScore(score: Score): Promise<void> {
    const newScore = await this.scoreService.postScore(score);
    console.log(newScore);
  }

  async changeScoreVisibility(score: Score) {
    score.visibilite = !score.visibilite;
  }

}
