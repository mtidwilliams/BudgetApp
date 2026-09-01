import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = '/auth';
  email: string | undefined;

  constructor(private http: HttpClient, private router: Router) {  }

  getToken(): string | null {
    let token = localStorage.getItem('authToken');
    if(!token || this.isTokenExpired(token)) {
      this.logout();
      return null;
    }
    return token;
  }

  isTokenExpired(token: string): boolean {
    try {
      let payload = JSON.parse(atob(token.split('.')[1]));
      let expiry = payload.exp * 1000; //converts the time to milliseconds
      return Date.now() > expiry;
    } catch (error) {
      return true;
    }
  }

  removeToken() {
    localStorage.removeItem('authToken');
  }

  login(email: string, password: string): Observable<any> {
    this.email = email;
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password });
  }

  register(firstName: string, lastName: string, street: string, city: string, state: string, zip: string, email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, { firstName: firstName, lastName: lastName, street, city, state, zip, email, password });
  }

  logout(): boolean {
    this.removeToken();
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
    let token = this.getToken();
    if (!!token) {
        return true; // Allow access to the route
    } else {
        this.logout();
        return false; // Deny access to the route
    }
  }

  refreshToken(): void {
    const token = this.getToken();
    if (token) {
      let email = this.email;
      let refreshToken = this.http.post<any>(`${this.apiUrl}/refreshToken`, { email });
      refreshToken.subscribe(
        (result) => {
          localStorage.setItem('authToken', result.token);
        }
      );
    }
  }
}
