import { Component, OnInit } from '@angular/core';
import {User} from '../../models/user/user';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent implements OnInit {

  users: User[];
  headers: string[];

  constructor() { }

  ngOnInit() {
    this.headers = ['#', 'avatar', 'Username', 'Country', 'Points'];
  }

}
