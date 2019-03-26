import { Component, OnInit } from '@angular/core';
import {Thread} from '../../models/forum/thread';
import {Pagination} from '../../models/pagination/pagination';
import {ThreadService} from '../../_services/thread.service';
import {SearchService} from '../../_services/search.service';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {PaginatedResult} from '../../models/pagination/paginatedResult';
import {FormControl} from '@angular/forms';
import {FormValidatorOptions} from '../../utils/formValidatorOptions';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  threads: Thread[];
  pagination: Pagination;

  searchForm = new FormControl('', FormValidatorOptions.setStringOptions(true, 2, 25));

  constructor(private threadService: ThreadService,
              private searchService: SearchService,
              private route: ActivatedRoute,
              private router: Router,
              private toastr: ToastrService,
              public authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      console.log(data);
      this.threads = data['threads'].result;
      this.pagination = data['threads'].pagination;
    });
  }

  searchForum() {
    if (this.searchForm.valid) {
      const regex = this.searchForm.value;

      this.searchService.search(regex, this.pagination.currentPage, this.pagination.itemsPerPage)
        .subscribe((res: PaginatedResult<Thread[]>) => {
          this.threads = res.result;
          this.pagination = res.pagination;
          this.toastr.info(`Found in ${this.pagination.totalItems} threads.`);
          }, error => {
          this.toastr.error(error);
        });
    }
  }

}
