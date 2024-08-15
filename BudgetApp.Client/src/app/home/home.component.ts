import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private router: Router, private toastr: ToastrService ) {
    // Check if the state exists and show the toast message
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras?.state?.['showToast']) {
      const message = navigation.extras.state['toastMessage'] || 'Action was successful';
      const title = navigation.extras.state['toastTitle'] || 'Info';
      this.toastr.success(message, title);
    }
  }
}
