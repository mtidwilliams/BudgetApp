import { ToastrService } from 'ngx-toastr';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  username: string = '';
  password: string = '';
  firstName: string = '';
  error: string = '';

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService, private route: ActivatedRoute) {
    // Check if the state exists and show the toast message
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras?.state?.['showToast']) {
      const message = navigation.extras.state['toastMessage'] || 'Action was successful';
      const title = navigation.extras.state['toastTitle'] || 'Info';
      this.toastr.success(message, title);
    }
  }

  login() {
    this.authService.login(this.username, this.password).subscribe(
      result => {
        localStorage.setItem('authToken', result.token);
        this.router.navigate(
          ['/home'], {
            state: {
              showToast: true,
              toastMessage: 'Welcome to Build-a-Budget '+(result.firstName ?? '')+'.',
              toastTitle: 'Success!'
            }
          }
        );
      },
      error => {
        this.error = 'Login failed';
      }
    );
  }
}
