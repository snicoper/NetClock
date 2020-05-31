import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { LocalizationService } from '../services';

@Injectable()
export class CultureInterceptor implements HttpInterceptor {
  constructor(private localizationService: LocalizationService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (!request.headers.has('Content-Language')) {
      request = request.clone({
        headers: request.headers.set('Accept-Language', this.localizationService.getCurrentCultureValue())
      });
    }

    return next.handle(request);
  }
}
