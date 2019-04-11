import { Component, OnInit } from '@angular/core';
import {User} from '../../models/user/user';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent implements OnInit {

  users = [{
    'avatar': 'http://res.cloudinary.com/dpvvbbotx/image/upload/v1554983005/amka9kygdfbsanpwciwh.jpg',
    'username': 'Henry',
    'points': 99
  }, {
    'avatar': 'http://res.cloudinary.com/dpvvbbotx/image/upload/v1554983550/dphehxxccbwgr2lzr0qr.jpg',
    'username': 'John',
    'points': 81
  }, {
    'avatar': 'http://res.cloudinary.com/dpvvbbotx/image/upload/v1554982358/hb7zivwg2jfp6liuk7ks.jpg',
    'username': 'Alice',
    'points': 67
  }, {
    'avatar': 'http://res.cloudinary.com/dpvvbbotx/image/upload/v1554983206/nltttk7x0hqbvdx0jq0y.jpg',
    'username': 'Jeannie',
    'points': 54
  }, {
    'avatar': 'https://randomuser.me/api/portraits/women/43.jpg',
    'username': 'Anna',
    'points': 50
  }, {
    'avatar': 'http://res.cloudinary.com/dpvvbbotx/image/upload/v1554983816/rrh3or3abnshcjfbcqvg.jpg',
    'username': 'Rick',
    'points': 44
  }, {
    'avatar': 'https://randomuser.me/api/portraits/men/22.jpg',
    'username': 'Thomas',
    'points': 36
  }, {
    'avatar': 'http://res.cloudinary.com/dpvvbbotx/image/upload/v1554983445/gacswndmwn1tutfnqhti.jpg',
    'username': 'Vanessa',
    'points': 20
  }];
  headers: string[];

  constructor() { }

  ngOnInit() {
    this.headers = ['#', 'User', 'Points'];
  }

}
