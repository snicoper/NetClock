import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StatusCodes } from 'http-status-codes';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { DebugErrors, SiteUrls } from '../core';
import { AuthService } from '../pages/auth/login/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthService, private router: Router) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(error => {
      DebugErrors(error.error);

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
    }));
  }

  private unauthorizedHandler(): void {
    this.authenticationService.logout();
    this.router.navigate([SiteUrls.authLogin]);
  }

  private forbiddenHandler(): void {
    this.router.navigate([SiteUrls.errorsForbidden]);
  }

  private errorHandler(): void {
    // Nothing.
  }
}
