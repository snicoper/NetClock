import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StatusCodes } from 'http-status-codes';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../../pages/auth/login/auth.service';
import { debugErrors, siteUrls } from '../common';

@Injectable()
export class ErrorRequestInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthService, private router: Router) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error) => {
        debugErrors(error.error);

        switch (error.status) {
          case StatusCodes.UNAUTHORIZED:
            this.unauthorizedHandler();
            break;
          case StatusCodes.FORBIDDEN:
            this.forbiddenHandler();
            break;
          case StatusCodes.BAD_REQUEST:
            this.errorHandler();
            break;
        }

        return throwError(error);
      })
    );
  }

  private unauthorizedHandler(): void {
    this.authenticationService.logout();
    this.router.navigate([siteUrls.authLogin]);
  }

  private forbiddenHandler(): void {
    this.router.navigate([siteUrls.errorsForbidden]);
  }

  private errorHandler(): void {
    // Nothing.
  }
}
