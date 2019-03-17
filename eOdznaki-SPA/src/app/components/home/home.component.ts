import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../../_services/auth.service';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  model: any = {};
  registerMode = false;

  constructor(private http: HttpClient, public authService: AuthService, private toastr: ToastrService,
              private router: Router) {
  }

  ngOnInit() {
    document.body.classList.add('background-img');
  }

  ngOnDestroy() {
    document.body.classList.remove('background-img');
  }

  registerToggle(registerMode: boolean) {
    this.registerMode = registerMode;
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
