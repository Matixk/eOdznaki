import {Injectable} from '@angular/core';
import {User} from '../models/user/user';
import {ActivatedRouteSnapshot, Resolve, Router} from '@angular/router';
import {UserService} from '../_services/user.service';
import {ToastrService} from 'ngx-toastr';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {AuthService} from '../_services/auth.service';
import {AdminService} from '../_services/admin.service';

@Injectable()
export class UserRolesResolver implements Resolve<any> {

  constructor(private adminService: AdminService, private authService: AuthService,
              private router: Router, private toastr: ToastrService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<any> {
    if (this.authService.roleMatch(['Admin'])) {
      return this.adminService.getUsersWithRoles().pipe(
        catchError(error => {
          this.toastr.toastrConfig.preventDuplicates = true;
          this.toastr.error('Could not retrieve Roles data.');
          this.router.navigate(['/home']);
          return of(null);
        })
      );
    }
  }
}
