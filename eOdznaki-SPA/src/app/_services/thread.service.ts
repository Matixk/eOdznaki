import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {Thread} from '../models/forum/thread';
import {PaginatedResult} from 'src/app/models/pagination/paginatedResult';
import {ThreadForCreate} from '../dtos/threadForCreate';

@Injectable({
  providedIn: 'root'
})
export class ThreadService {
  private threadsUrl = 'http://localhost:5000/api/ForumThreads';

  constructor(private http: HttpClient) { }

  getThread(id: number): Observable<Thread> {
    return this.http.get<Thread>(`${this.threadsUrl}/${id}`);
  }

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

  addThread(thread: ThreadForCreate): Observable<Thread> {
    console.log(thread);
    if (thread.title == null || thread.content == null) {
      throw new Error('Title and content need to be filled!');
    }
    if (thread.title.length > 50) {
      throw new Error('Max length of title is 50!');
    }
    if (thread.title.length < 5) {
      throw new Error('Min length of title is 5!');
    }
    if (thread.content.length > 2000) {
      throw new Error('Max length of content is 2000!');
    }
    if (thread.content.length < 5) {
      throw new Error('Min length of content is 5!');
    }

    return this.http.post<Thread>(this.threadsUrl, thread);
  }

  delete(id: number) {
    return this.http.delete<Thread>(`${this.threadsUrl}/${id}`);
  }
} 
