import { Component, OnInit } from '@angular/core';
import {User} from '../../models/user/user';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent implements OnInit {

  users = [{
    'avatar': 'https://robohash.org/eumdignissimosmagnam.png?size=40x40&set=set1',
    'username': 'rodowd0',
    'points': 99
  }, {
    'avatar': 'https://robohash.org/ablaudantiumratione.jpg?size=40x40&set=set1',
    'username': 'swinspire1',
    'points': 81
  }, {
    'avatar': 'https://robohash.org/voluptasnemodolore.bmp?size=40x40&set=set1',
    'username': 'wrickerd2',
    'points': 67
  }, {
    'avatar': 'https://robohash.org/minimaaliquidtenetur.png?size=40x40&set=set1',
    'username': 'gdowney3',
    'points': 54
  }, {
    'avatar': 'https://robohash.org/omnisfugalaborum.png?size=40x40&set=set1',
    'username': 'esegrott4',
    'points': 50
  }, {
    'avatar': 'https://robohash.org/quiiuresed.jpg?size=40x40&set=set1',
    'username': 'fvogel5',
    'points': 44
  }, {
    'avatar': 'https://robohash.org/officiissedcumque.bmp?size=40x40&set=set1',
    'username': 'fsimonaitis6',
    'points': 36
  }, {
    'avatar': 'https://robohash.org/laborequidicta.png?size=40x40&set=set1',
    'username': 'kboat7',
    'points': 20
  }, {
    'avatar': 'https://robohash.org/idnumquampossimus.jpg?size=40x40&set=set1',
    'username': 'gallchin8',
    'points': 16
  }, {
    'avatar': 'https://robohash.org/minusexcepturitotam.jpg?size=40x40&set=set1',
    'username': 'sarntzen9',
    'points': 14
  }];
  headers: string[];

  constructor() { }

  ngOnInit() {
    this.headers = ['#', 'User', 'Points'];
  }

}
