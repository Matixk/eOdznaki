import {Injectable} from '@angular/core';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {Post} from '../models/forum/post';
import {PostService} from '../_services/post.service';

@Injectable()
export class ThreadPostResolver implements Resolve<Post[]> {
  pageNumber = 1;
  pageSize = 10;

  constructor(private postService: PostService,
              private router: Router,
              private toastr: ToastrService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Post[]> {
    return this.postService.getPosts(route.params['id'], this.pageNumber, this.pageSize).pipe(
      catchError(error => {
        this.toastr.toastrConfig.preventDuplicates = true;
        this.toastr.error('Could not retrieve User data');
        this.router.navigate(['/forum']);
        return of(null);
      })
    );
  }
}
