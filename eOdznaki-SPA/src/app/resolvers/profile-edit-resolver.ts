import {Injectable} from '@angular/core';
import {User} from '../models/user';
import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {UserService} from '../_services/user.service';
import {ToastrService} from 'ngx-toastr';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {AuthService} from '../_services/auth.service';

@Injectable()
export class ProfileEditResolver implements Resolve<User> {
  constructor(private userService: UserService, private router: Router,
              private toastr: ToastrService, private authService: AuthService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<User> {
    return this.userService.getUser(this.authService.decodedToken.nameid).pipe(
      catchError(() => {
        this.toastr.toastrConfig.preventDuplicates = true;
        this.toastr.error('Could not retrieve profile data');
        this.router.navigate(['/home']);
        return of(null);
      })
    );
  }
}
