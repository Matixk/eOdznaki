import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';


  constructor(private toastr: ToastrService) { }

  ngOnInit() {
    this.test();
  }

  test() {
    this.toastr.info('Test message');
  }
}


