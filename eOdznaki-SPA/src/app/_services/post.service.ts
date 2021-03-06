import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {PaginatedResult} from 'src/app/models/pagination/paginatedResult';
import {Post} from '../models/forum/post';
import {PostForCreate} from '../dtos/postForCreate';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private postsUrl = 'http://localhost:5000/api/ForumPosts';

  constructor(private http: HttpClient) { }

  getPosts(threadId, page?, itemsPerPage?): Observable<PaginatedResult<Post[]>> {
    const paginatedResult: PaginatedResult<Post[]> = new PaginatedResult<Post[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Post[]>(`${this.postsUrl}/${threadId}`, { observe: 'response', params })
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

  answer(post: PostForCreate): Observable<Post> {
    return this.http.post<Post>(this.postsUrl, post);
}

  getOriginalPost(threadId, page?, itemsPerPage?): Observable<Post> {
    let params = new HttpParams();

    params = params.append('pageNumber', '1');
    params = params.append('pageSize', '1');

    return this.http.get<Post>(`${this.postsUrl}/${threadId}`, { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body[0];
        })
      );
  }

  delete(id: number) {
    return this.http.delete<Post>(`${this.postsUrl}/${id}`);
  }
}
