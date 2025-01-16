import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = '/auth';

  constructor(private http: HttpClient, private router: Router) { }

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password });
  }

  register(firstName: string, lastName: string, street: string, city: string, state: string, zip: string, email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, { firstName: firstName, lastName: lastName, street, city, state, zip, email, password });
  }

  logout(): boolean {
    localStorage.removeItem('authToken');
    this.router.navigate(['/'], {
      state: {
        showToast: true,
        toastTitle: 'Success!',
        toastMessage: 'You have been logged out.',
      }
    });
    return true;
  }

  canActivate(): boolean {
    const token = localStorage.getItem('authToken');
    if (!!token) {
        return true; // Allow access to the route
    } else {
        this.router.navigate(['/']); // Redirect to login page
        return false; // Deny access to the route
    }
  }
}
