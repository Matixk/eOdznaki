import {Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {User} from '../../models/user';
import {ActivatedRoute} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {UserService} from '../../_services/user.service';
import {AuthService} from '../../_services/auth.service';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.scss']
})
export class ProfileEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  user: User;

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private toastr: ToastrService,
              private userService: UserService, private authService: AuthService) {
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
    this.toastr.toastrConfig.preventDuplicates = true;
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

}
