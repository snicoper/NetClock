import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LocalizationService } from '../services';

@Injectable()
export class LocalizationResponseInterceptor implements HttpInterceptor {
  constructor(private localizationService: LocalizationService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Culture.
    if (!request.headers.has('Accept-Language')) {
      request = request.clone({
        headers: request.headers.set('Accept-Language', this.localizationService.getCultureValue())
      });
    }

    // Timezone.
    if (!request.headers.has('Accept-Timezone')) {
      request = request.clone({
        headers: request.headers.set('Accept-Timezone', this.localizationService.getTimezoneValue())
      });
    }

    // Timezone offset.
    if (!request.headers.has('Accept-UtcOffset')) {
      request = request.clone({
        headers: request.headers.set('Accept-UtcOffset', this.localizationService.utcOffset().toString())
      });
    }

    return next.handle(request);
  }
}
