import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StatusCodes } from 'http-status-codes';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiResultItemFilter, ApiResultItemOrderBy } from '../models';

/** Comprueba si es un ApiResult y deserializa filters. */
@Injectable()
export class ApiResultRequestInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        map(event => {
          if (event instanceof HttpResponse && event.status === StatusCodes.OK) {
            // Orders de ApiResult.
            if ('orders' in event.body) {
              event.body.orders = event.body.orders === ''
                ? []
                : event.body.orders = JSON.parse(event.body.orders) as ApiResultItemOrderBy;
            }

            // Filtros de ApiResult.
            if ('filters' in event.body) {
              event.body.filters = event.body.filters === ''
                ? []
                : event.body.filters = JSON.parse(event.body.filters) as ApiResultItemFilter;
            }
          }

          return event;
        }));
  }
}
