import {Component, OnInit, ViewChild} from '@angular/core';
import {Pagination} from '../../models/pagination/pagination';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Post} from '../../models/forum/post';
import {PaginatedResult} from '../../models/pagination/paginatedResult';
import {PostService} from '../../_services/post.service';
import {ThreadService} from '../../_services/thread.service';
import {Thread} from '../../models/forum/thread';
import {AuthService} from '../../_services/auth.service';
import {FormControl, NgForm} from '@angular/forms';
import {FormValidatorOptions} from '../../utils/formValidatorOptions';
import {PostForCreate} from '../../dtos/postForCreate';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {

  loading = false;
  thread: Thread;
  originalPost: Post;
  posts: Post[];
  pagination: Pagination;
  postForm = new FormControl('', FormValidatorOptions.setStringOptions(true, 5, 2000));
  @ViewChild('newPost') public newPostModal;
  @ViewChild('deletePost') public deletePostModal;
  private postToDelete: number;
  hoveredIndex: number;

  constructor(private postService: PostService,
              private threadService: ThreadService,
              private route: ActivatedRoute,
              private toastr: ToastrService,
              public authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.posts = data['posts'].result;
      this.pagination = data['posts'].pagination;
    });
    this.route.params.subscribe(params => {
        this.threadService.getThread(params['id'])
          .subscribe(threadSub => {
            this.thread = threadSub;
          }, error => {
            console.log(error);
            this.toastr.error('Couldn\'t load thread');
          });

        this.postService.getOriginalPost(params['id'])
          .subscribe(res => {
            this.originalPost = res;
            },  error => {
            console.log(error);
            this.toastr.error('Couldn\'t load original post');
        });
    });
  }

  loadPosts() {
    if (this.pagination.currentPage === 1) {
      this.pagination.itemsPerPage = 5;
    } else {
      this.pagination.itemsPerPage = 4;
    }
    this.postService.getPosts(this.thread.id, this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe((res: PaginatedResult<Post[]>) => {
        this.posts = res.result;
        if (this.pagination.currentPage !== 1) {
          this.posts.unshift(this.originalPost);
        }
        this.pagination = res.pagination;
      }, error => {
        console.log(error);
        this.toastr.error('Couldn\'t load posts');
      });
  }

  goToPage(n: number): void {
    this.pagination.currentPage = n;
    this.loadPosts();
  }

  nextPage(): void {
    this.pagination.currentPage++;
    this.loadPosts();
  }

  prevPage(): void {
    this.pagination.currentPage--;
    this.loadPosts();
  }

  answer() {
    if (this.postForm.valid) {
      const post = new PostForCreate(this.thread.id, this.postForm.value);
      this.postService.answer(post).subscribe(next => {
        this.newPostModal.hide();
        this.pagination.currentPage = this.pagination.totalPages;
        this.loadPosts();
        this.toastr.success('Created post successfully.');
      }, error => {
        console.log(error);
        this.toastr.error(error === 'NotFound' ? 'Invalid user.' : 'Failed to create.');
      });
    }
  }

  clearForm() {
    this.postForm.setValue('');
  }

  showDeleteWarning(id: number) {
    this.postToDelete = id;
    this.deletePostModal.show();
  }

  delete() {
    this.postService.delete(this.postToDelete).subscribe(() => {
      this.deletePostModal.hide();
      this.loadPosts();
      this.toastr.success('Deleted post successfully.');
    }, error => {
      console.log(error);
      this.toastr.error(error === 'NotFound' ? 'Invalid user.' : 'Failed to create.');
    });
  }

  quote(content: string, authorName: string) {
    this.postForm.setValue(`\`\`\`\n${content}\n\`\`\` ~ ${authorName}\n`);
    this.newPostModal.show();
  }
}
