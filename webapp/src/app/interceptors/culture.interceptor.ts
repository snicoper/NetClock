import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class CultureInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // TODO: Obtener culture desde localStorage o servicio de localización.
    const culture = 'es';
    if (!request.headers.has('Content-Language')) {
      request = request.clone({headers: request.headers.set('Accept-Language', culture)});
    }

    return next.handle(request);
  }
}
