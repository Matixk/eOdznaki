import {Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {User} from '../../models/user';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {UserService} from '../../_services/user.service';
import {AuthService} from '../../_services/auth.service';
import {environment} from '../../../environments/environment';
import {FileUploader} from 'ng2-file-upload';
import {ModalDirective} from 'angular-bootstrap-md';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.scss']
})
export class ProfileEditComponent implements OnInit {
  baseUrl = environment.apiUrl;
  @ViewChild('basicModal') basicModal: ModalDirective;
  @ViewChild('editForm') editForm: NgForm;
  uploader: FileUploader;
  user: User;
  avatarUrl;

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private toastr: ToastrService,
              private userService: UserService, private authService: AuthService) {
    this.authService.currentAvatarUrl.subscribe(avatarUrl => this.avatarUrl = avatarUrl);
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
    this.toastr.toastrConfig.preventDuplicates = true;
    this.initializeUploader();
  }

  ngOnDestroy() {
    this.toastr.toastrConfig.disableTimeOut = false;
    this.toastr.clear();
  }

  updateUser() {
    this.toastr.clear();
    this.toastr.toastrConfig.easeTime = 300;
    this.toastr.toastrConfig.disableTimeOut = false;
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.toastr.success('Profile updated successfully.');
    }, error => {
      error === 'No changes were detected.' ? this.toastr.info(error) : this.toastr.error(error);
    });
    this.editForm.reset(this.user);
  }

  changesNotification() {
    this.toastr.toastrConfig.easeTime = 0;
    this.toastr.toastrConfig.disableTimeOut = true;
    this.toastr.info('Any unsaved changes will be lost.');
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/setAvatar',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
      if (this.uploader.queue.length > 1) {
        this.uploader.removeFromQueue(this.uploader.queue[0]);
      }
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      this.basicModal.hide();
      this.toastr.toastrConfig.easeTime = 300;
      this.toastr.toastrConfig.disableTimeOut = false;
      this.toastr.success('Avatar changed successfully.');
      if (response) {
        const res: User = JSON.parse(response);
        const user = {
          avatarUrl: res.avatarUrl,
        };
        this.authService.setAvatar(user.avatarUrl);
        this.authService.currentUser.avatarUrl = user.avatarUrl;
        localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
      }
    };
  }
}
