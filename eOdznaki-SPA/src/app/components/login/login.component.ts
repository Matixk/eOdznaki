import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthService} from '../../_services/auth.service';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  @Output() register = new EventEmitter();
  model: any = {};

  constructor(private http: HttpClient, public authService: AuthService, private toastr: ToastrService,
              private router: Router) {
  }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.toastr.info('Logged in successfully');
    }, error => {
      console.log(error.stat);
      this.toastr.error(error === 'Unauthorized' ? 'Invalid Username or Password' : 'Failed to login');
    }, () => {
      this.router.navigate(['/']);
    });
  }

  registerButton() {
    this.register.emit(true);
  }

}
