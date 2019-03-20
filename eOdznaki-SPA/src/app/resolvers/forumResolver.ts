import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot} from '@angular/router';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';

import {Thread} from '../models/forum/thread';
import {ThreadService} from '../_services/thread.service';

@Injectable()
export class ThreadsResolver implements Resolve<Thread[]> {
  pageNumber = 1;
  pageSize = 4;

  constructor(
    private threadService: ThreadService,
    private router: Router,
    private toastr: ToastrService) {
  }

  resolve(route: ActivatedRouteSnapshot): Observable<Thread[]> {
    return this.threadService.getThreads(this.pageNumber, this.pageSize).pipe(
      catchError(error => {
        this.toastr.error('Could not retrieve Threads data.');
        this.router.navigate(['/home']);
        return of(null);
      })
    );
  }
}
