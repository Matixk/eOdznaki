import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Thread } from '../models/forum/thread';
import { PaginatedResult } from 'src/app/models/pagination/paginatedResult';

@Injectable({
  providedIn: 'root'
})
export class ThreadService {
  private threadsUrl = 'http://localhost:5000/api/ForumThreads';

  constructor(private http: HttpClient) { }

  getThreads(page?, itemsPerPage?): Observable<PaginatedResult<Thread[]>> {
    const paginatedResult: PaginatedResult<Thread[]> = new PaginatedResult<Thread[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Thread[]>(this.threadsUrl, { observe: 'response', params })
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
