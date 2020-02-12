import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';

import { DebugConsole } from '../core';
import { RequestData } from '../models';

export abstract class ApiBaseService implements OnInit {
  protected baseUrl: string;

  protected constructor(protected http: HttpClient) {
  }

  public ngOnInit(): void {
    if (!this.baseUrl) {
      DebugConsole.raiseTypeError('baseUrl no puede estar vacío.');
    }
  }

  protected prepareQueryParams<TModel>(requestData: RequestData<TModel>): string {
    return this.requestDataToQueryParams(requestData);
  }

  protected requestDataToQueryParams<TModel>(requestData: RequestData<TModel>): string {
    let queryParams = '';
    queryParams += `totalItems=${requestData.totalItems}`;
    queryParams += `&pageNumber=${requestData.pageNumber}`;
    queryParams += `&totalPages=${requestData.totalPages}`;
    queryParams += `&pageSize=${requestData.pageSize}`;

    if (requestData.sorts) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `sorts=${requestData.sorts}`;
    }

    if (requestData.filters) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `filters=${requestData.filters}`;
    }

    return queryParams;
  }

  protected concatQueryParam(queryParams: string): string {
    return queryParams !== '' ? '&' : '';
  }
}
