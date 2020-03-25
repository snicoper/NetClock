import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as HttpStatus from 'http-status-codes';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SiteUrls } from '../core';

import { AuthRestService } from '../pages/auth/services/auth-rest.service';
import { DebugService } from '../services';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private authenticationService: AuthRestService,
    private router: Router,
    private debugService: DebugService
  ) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(error => {
      this.debugService.errors(error);

      switch (error.status) {
        case HttpStatus.UNAUTHORIZED:
          this.unauthorizedHandler();
          break;
        case HttpStatus.BAD_REQUEST:
          this.errorHandler();
          break;
        default:
        // Nothing.
      }

      return throwError(error);
    }));
  }

  private unauthorizedHandler(): void {
    this.authenticationService.logout();
    this.router.navigate([SiteUrls.login]);
  }

  private errorHandler(): void {
    // Nothing.
  }
}
