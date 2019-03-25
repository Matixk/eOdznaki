import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {JwtHelperService} from '@auth0/angular-jwt';
import {User} from '../models/user/user';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  avatarUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentAvatarUrl = this.avatarUrl.asObservable();

  constructor(private http: HttpClient) {
  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user.user));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.user;
          this.setAvatar(this.currentUser.avatarUrl);
        }
      })
    );
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    if (this.decodedToken != null) {
      const userRoles = this.decodedToken.role as Array<String>;
      allowedRoles.forEach(role => {
        if (userRoles && userRoles.includes(role)) {
          isMatch = true;
          return;
        }
      });
    }
    return isMatch;
  }

  setAvatar(avatarUrl: string) {
    this.avatarUrl.next(avatarUrl);
  }
}
