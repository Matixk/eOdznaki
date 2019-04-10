import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {JwtHelperService} from '@auth0/angular-jwt';
import {HttpClient} from '@angular/common/http';
import {Trail} from '../dtos/trail';
import {PaginatedResult} from '../models/pagination/paginatedResult';
import {Observable} from 'rxjs';
import {HttpParams} from '@angular/common/http';
import {map} from 'rxjs/operators';

@Injectable ({
  providedIn: 'root'
})
export class TrailService {
  baseUrl = environment.apiUrl + 'Trails/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) {
  }

  getTrail(id: number): Observable<Trail> {
    return this.http.get<Trail>(`${this.baseUrl}/${id}`);
  }

  getTrails(page?, itemsPerPage?): Observable<PaginatedResult<Trail[]>> {
    const paginatedResult: PaginatedResult<Trail[]> = new PaginatedResult<Trail[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Trail[]>(this.baseUrl, { observe: 'response', params })
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

  addTrail(trail: Trail): Observable<Trail> {
    console.log(trail);

    return this.http.post<Trail>(this.baseUrl, trail);
  }
}
