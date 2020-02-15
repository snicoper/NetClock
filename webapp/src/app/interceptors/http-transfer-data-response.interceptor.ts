import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as HttpStatus from 'http-status-codes';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

/** Comprueba si es un HttpTransferData y deserializa filters. */
@Injectable()
export class HttpTransferDataResponseInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(map(event => {
      if (event instanceof HttpResponse && event.status === HttpStatus.OK) {
        if ('filters' in event.body && event.body.filters !== '') {
          event.body.filters = JSON.parse(event.body.filters);
        }
      }

      return event;
    }));
  }
}
