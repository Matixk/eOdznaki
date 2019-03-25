import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PaginatedResult} from '../models/pagination/paginatedResult';
import {map} from 'rxjs/operators';
import {UserWithRoles} from '../models/user/user-with-roles';
import {User} from '../models/user/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getUsersWithRoles(page?, itemsPerPage?): Observable<PaginatedResult<UserWithRoles[]>> {
    const paginatedResult: PaginatedResult<UserWithRoles[]> = new PaginatedResult<UserWithRoles[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<UserWithRoles[]>(this.baseUrl + 'admin/users', {observe: 'response', params})
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );

  }

  updateUserRoles(user: UserWithRoles, roles: {}) {
    return this.http.post(this.baseUrl + 'admin/editRoles/' + user.id, roles);
  }
}
