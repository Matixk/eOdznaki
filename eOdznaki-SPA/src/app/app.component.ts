import {Component} from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
import {AuthService} from './_services/auth.service';
import {User} from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'eOdznaki';

  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService) {
  }

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
    if (user) {
      this.authService.currentUser = user;
    }
  }

}


