import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {PaginatedResult} from '../models/pagination/paginatedResult';
import {Thread} from '../models/forum/thread';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private searchUrl = 'http://localhost:5000/api/Search';

  constructor(private http: HttpClient) {}

  search(regex: string, page?, itemsPerPage?): Observable<PaginatedResult<Thread[]>> {
    const paginatedResult: PaginatedResult<Thread[]> = new PaginatedResult<Thread[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    params = params.append('regex', regex);

    return this.http.get<Thread[]>(this.searchUrl, { observe: 'response', params })
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

}
