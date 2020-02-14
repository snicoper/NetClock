import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';

import { DebugConsole } from '../core';
import { LogicalOperator, RelationalOperator, RequestData } from '../models';

export abstract class ApiBaseService implements OnInit {
  protected baseUrl: string;

  protected constructor(protected http: HttpClient) {
  }

  ngOnInit(): void {
    if (!this.baseUrl) {
      DebugConsole.raiseError('baseUrl no puede estar vacío.');
    }
  }

  protected prepareQueryParams<TModel>(requestData: RequestData<TModel>): string {
    return this.requestDataToQueryParams(requestData);
  }

  protected requestDataToQueryParams<TModel>(requestData: RequestData<TModel>): string {
    // TODO: Test filter, delete.
    requestData.addFilter('userName', RelationalOperator.contains, 'ad');
    requestData.addFilter('userName', RelationalOperator.contains, 'o', LogicalOperator.or);

    let queryParams = '';
    queryParams += `totalItems=${requestData.totalItems}`;
    queryParams += `&pageNumber=${requestData.pageNumber}`;
    queryParams += `&totalPages=${requestData.totalPages}`;
    queryParams += `&pageSize=${requestData.pageSize}`;

    if (requestData.sorts) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `sorts=${requestData.sorts}`;
    }

    if (requestData.filters.length) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams = `filters=${requestData.stringifyFilters()}`
    }

    return queryParams;
  }

  protected concatQueryParam(queryParams: string): string {
    return queryParams !== '' ? '&' : '';
  }
}
