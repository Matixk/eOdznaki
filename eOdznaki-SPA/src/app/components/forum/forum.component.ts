import {Component, OnInit} from '@angular/core';
import {Thread} from '../../models/forum/thread';
import {Pagination} from '../../models/pagination/pagination';
import {ActivatedRoute, Router} from '@angular/router';
import {ThreadService} from '../../_services/thread.service';
import {ToastrService} from 'ngx-toastr';
import {PaginatedResult} from '../../models/pagination/paginatedResult';
import {FormControl, FormGroup} from '@angular/forms';
import {AuthService} from '../../_services/auth.service';
import {ThreadForCreate} from '../../dtos/threadForCreate';
import {SearchService} from '../../_services/search.service';
import {FormValidatorOptions} from '../../utils/formValidatorOptions';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.scss']
})
export class ForumComponent implements OnInit {

  threads: Thread[];
  pagination: Pagination;

  threadForm = new FormGroup({
    title: new FormControl('', FormValidatorOptions.setStringOptions(true, 5, 50)),
    content: new FormControl('', FormValidatorOptions.setStringOptions(true, 5, 2000))
  });

  searchForm = new FormControl('', FormValidatorOptions.setStringOptions(true, 2, 25));

  constructor(private threadService: ThreadService,
              private searchService: SearchService,
              private route: ActivatedRoute,
              private router: Router,
              private toastr: ToastrService,
              public authService: AuthService) { }

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
        console.log(this.threads[0].forumPosts);
        this.pagination = res.pagination;
      }, error => {
        this.toastr.error(error);
      });
  }

  addThread() {
    if (this.threadForm.valid) {
      let threadId;
      const form = this.threadForm;
      const threadForCreate = new ThreadForCreate(form.get('title').value, form.get('content').value);

      this.threadService.addThread(threadForCreate).subscribe(next => {
        threadId = next.id;
        this.toastr.info('Created');
      }, error => {
        console.log(error.stat);
        this.toastr.error(error === 'NotFound' ? 'Invalid user' : 'Failed to create');
      }, () => {
        this.router.navigate(['/forum', threadId]);
      });
    }
  }

  searchForum() {
    if (this.searchForm.valid) {
      this.router.navigate(['/search'], this.searchForm.value);
    }
  }

}
