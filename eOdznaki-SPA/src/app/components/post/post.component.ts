import { Component, OnInit } from '@angular/core';
import {Pagination} from '../../models/pagination/pagination';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Post} from '../../models/forum/post';
import {PaginatedResult} from '../../models/pagination/paginatedResult';
import {PostService} from '../../_services/post.service';
import {ThreadService} from '../../_services/thread.service';
import {Thread} from '../../models/forum/thread';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {

  thread: Thread;
  posts: Post[];
  pagination: Pagination;

  constructor(private postService: PostService,
              private threadService: ThreadService,
              private route: ActivatedRoute,
              private toastr: ToastrService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.posts = data['posts'].result;
      this.pagination = data['posts'].pagination;
    });
    this.route.params.subscribe(params =>
      this.threadService.getThread(params['id'])
        .subscribe(threadSub => this.thread = threadSub)
    );
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadPosts();
  }

  loadPosts() {
    this.postService.getPosts(this.route.params['id'], this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((res: PaginatedResult<Post[]>) => {
        this.posts = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.toastr.error(error);
      });
  }

}
