import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot} from '@angular/router';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';

import {Thread} from '../models/forum/thread';
import {SearchService} from '../_services/search.service';

@Injectable()
export class SearchResolver implements Resolve<Thread[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private searchService: SearchService,
    private router: Router,
    private toastr: ToastrService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Thread[]> {
    return this.searchService.search(route.params['regex'], this.pageNumber, this.pageSize).pipe(
      catchError(error => {
        this.toastr.error('Could not retrieve Threads data.');
        this.router.navigate(['/forum']);
        return of(null);
      })
    );
  }
}
