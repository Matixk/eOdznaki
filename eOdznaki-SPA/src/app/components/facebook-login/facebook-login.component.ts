import { Component, OnInit, ViewChild } from '@angular/core';
import { Thread } from '../../models/forum/thread';
import { Pagination } from '../../models/pagination/pagination';
import { ActivatedRoute, Router } from '@angular/router';
import { ThreadService } from '../../_services/thread.service';
import { ToastrService } from 'ngx-toastr';
import { PaginatedResult } from '../../models/pagination/paginatedResult';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from '../../_services/auth.service';
import { ThreadForCreate } from '../../dtos/threadForCreate';
import { SearchService } from '../../_services/search.service';
import { FormValidatorOptions } from '../../utils/formValidatorOptions';

@Component({
  selector: 'app-facebook-login',
  templateUrl: './facebook-login.component.html',
  styleUrls: ['./facebook-login.component.scss']
})
export class FacebookLoginComponent {

  private authWindow: Window;
  failed: boolean;
  error: string;
  errorDescription: string;
  isRequesting: boolean;

  launchFbLogin() {
    // launch facebook login dialog
    this.authWindow = window.open('https://www.facebook.com/v2.11/dialog/oauth?&response_type=token&display=popup&client_id=1528751870549294&display=popup&redirect_uri=http://localhost:5000/facebook-auth.html&scope=email', null, 'width=600,height=400');
  }

}
