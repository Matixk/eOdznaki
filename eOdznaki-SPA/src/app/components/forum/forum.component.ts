import { Component, OnInit } from '@angular/core';
import { Thread } from '../../models/forum/thread';
import { Pagination } from '../../models/pagination/pagination';
import { ActivatedRoute } from '@angular/router';
import { ThreadService } from '../../_services/thread.service';
import { ToastrService } from 'ngx-toastr';
import {PaginatedResult} from '../../models/pagination/paginatedResult';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.scss']
})
export class ForumComponent implements OnInit {

  threads: Thread[];
  pagination: Pagination;

  constructor(private threadService: ThreadService,
              private route: ActivatedRoute,
              private toastr: ToastrService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.threads = data['threads'].result;
      this.pagination = data['threads'].pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadThreads();
  }

  loadThreads() {
    this.threadService.getThreads(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((res: PaginatedResult<Thread[]>) => {
        this.threads = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.toastr.error(error);
      });
  }

}
