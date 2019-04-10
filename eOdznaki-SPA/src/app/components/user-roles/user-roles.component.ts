import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {AdminService} from '../../_services/admin.service';
import {ToastrService} from 'ngx-toastr';
import {UserWithRoles} from '../../models/user/user-with-roles';
import {MDBModalRef, MDBModalService} from 'angular-bootstrap-md';
import {RolesModalComponent} from '../roles-modal/roles-modal.component';
import {User} from '../../models/user/user';


@Component({
  selector: 'app-user-roles',
  templateUrl: './user-roles.component.html',
  styleUrls: ['./user-roles.component.scss']
})
export class UserRolesComponent implements OnInit {
  @Input() users: any;
  modalRef: MDBModalRef;
  usersWithRoles: any;
  hoveredIndex: number;

  constructor(private route: ActivatedRoute, private adminService: AdminService,
              private toastr: ToastrService, private modalService: MDBModalService) {
    this.usersWithRoles = this.route.snapshot.data['users'];
  }


  ngOnInit() {
  }

  editRolesModal(user: User) {

    const initialState = {
      backdrop: true,
      keyboard: true,
      focus: true,
      show: false,
      ignoreBackdropClick: false,
      class: 'modal-dialog-centered',
      containerClass: '',
      animated: true,
      data: {
        user: user,
        roles: this.getRolesArray(user)
      },
    };

    this.modalRef = this.modalService.show(RolesModalComponent, initialState);
    this.modalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
      };

      if (rolesToUpdate) {
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames];
          this.toastr.success(user.userName + '\'s roles have been updated.');
        }, error => {
          this.toastr.error(error);
        });
      }
    });
  }


  private getRolesArray(user) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      {name: 'Admin', value: 'Admin'},
      {name: 'Moderator', value: 'Moderator'},
      {name: 'Member', value: 'Member'},
    ];

    for (let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      for (let j = 0; j < userRoles.length; j++) {
        if (availableRoles[i].name === userRoles[j]) {
          isMatch = true;
          availableRoles[i].checked = true;
          roles.push(availableRoles[i]);
          break;
        }
      }
      if (!isMatch) {
        availableRoles[i].checked = false;
        roles.push(availableRoles[i]);
      }
    }
    return roles;
  }
}
