import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {JwtHelperService} from '@auth0/angular-jwt';
import {User} from '../models/user';
import {HttpClient} from '@angular/common/http';

@Injectable ({
  providedIn: 'root'
})
export class TrailService {
  baseUrl = environment.apiUrl + 'trail/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;

  constructor(private http: HttpClient) {
  }
}
