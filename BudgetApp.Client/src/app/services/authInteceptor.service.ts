import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { EMPTY, Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let isLoginReq = req.url.includes('/login');
    let isRefreshTokenReq = req.url.includes('/refreshToken');
    let isRegisterReq = req.url.includes('/register');

    // If the request is for logging in or registering your account, we won't have a token just yet, so just move on; we'll check it at the right time
    if(isLoginReq || isRegisterReq || isRefreshTokenReq) {
      return next.handle(req);
    }

    let allowed = this.authService.canActivate(); // checks if we can be here by verifying the token; logs out if not allowed

    if(!allowed) {
      return EMPTY; //stop the request
    }

    // If I've made it here, then my token isn't expired and I can make the request. Let's refresh the token since activity is being done in the app
    // otherwise, if they were idle for too long and are trying to make another request, then the "canActivate" call above will log them out
    this.authService.refreshToken();

    const clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authService.getToken()}`
      }
    });
    return next.handle(clonedRequest);
  }
}
