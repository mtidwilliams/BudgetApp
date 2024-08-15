import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  error: string = '';

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) { }

  register(): any {
    if(this.password != this.confirmPassword) {
      this.error = "Passwords do not match."
    } else {
      this.authService.register(this.firstName, this.lastName, this.email, this.password).subscribe(
        result => {
          this.router.navigate(
            ['/'], {
              state: {
                showToast: true,
                toastMessage: 'Your account has been created. Please log in using your new credentials.',
                toastTitle: 'Success!'
              }
            }
          );
        },
        error => {
          this.toastr.error(error.error.message, 'Unable to create new user.');
        }
      );
    }
  }
}
