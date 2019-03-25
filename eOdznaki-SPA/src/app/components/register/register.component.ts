import {Component, ElementRef, EventEmitter, Inject, OnInit, Output, PLATFORM_ID, ViewChild} from '@angular/core';
import {AuthService} from '../../_services/auth.service';
import {ToastrService} from 'ngx-toastr';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {User} from '../../models/user/user';
import {isPlatformBrowser} from '@angular/common';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  @ViewChild('submit') myInput: ElementRef;
  registerForm: FormGroup;
  user: User;

  constructor(private authService: AuthService, private toastr: ToastrService,
              private formBuilder: FormBuilder, private router: Router, @Inject(PLATFORM_ID) private platformId: Object) {
  }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.pattern('^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$')]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(64),
        Validators.pattern('((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,64})')]],
      confirmPassword: ['', Validators.compose(
        [Validators.required, this.validateAreEqual.bind(this)]
      )]
    });
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.toastr.success('Profile registered successfully');
      }, error => {
        console.log(error.error);
        this.toastr.error(error === 'Failed : DuplicateUserName' ? 'This Username is already taken.' : error);
      }, () => {
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/']);
          this.cancelRegister.emit(false);
        });
      });
    }
  }

  private validateAreEqual(fieldControl: FormControl) {
    if (this.registerForm !== undefined) {
      return fieldControl.value === this.registerForm.get('password').value ? null : {
        NotEqual: true
      };
    }
  }

  cancelRegisterButton() {
    this.cancelRegister.emit(false);
  }

  setFocus() {
    if (isPlatformBrowser(this.platformId)) {
      this.myInput.nativeElement.focus();
    }
  }

  get username() {
    return this.registerForm.get('username');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }

}
