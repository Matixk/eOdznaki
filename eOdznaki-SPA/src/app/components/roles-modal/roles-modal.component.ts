import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {User} from '../../models/user/user';
import {MDBModalRef} from 'angular-bootstrap-md';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.scss']
})

export class RolesModalComponent implements OnInit {
  @Output() updateSelectedRoles = new EventEmitter();
  user: User;
  roles: any[];

  constructor(public modalRef: MDBModalRef) {
  }

  ngOnInit() {
  }

  updateRoles() {
    this.updateSelectedRoles.emit(this.roles);
    this.modalRef.hide();
  }

}
