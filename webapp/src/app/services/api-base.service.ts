import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';

import { DebugConsole } from '../core';
import { HttpTransferData } from '../models';

export abstract class ApiBaseService implements OnInit {
  protected baseUrl: string;

  protected constructor(protected http: HttpClient) {
  }

  ngOnInit(): void {
    if (!this.baseUrl) {
      DebugConsole.raiseError('baseUrl no puede estar vacío.');
    }
  }

  protected prepareQueryParams<TModel>(transferData: HttpTransferData<TModel>): string {
    return this.transferDataToQueryParams(transferData);
  }

  protected transferDataToQueryParams<TModel>(transferData: HttpTransferData<TModel>): string {
    let queryParams = '';
    queryParams += `totalItems=${transferData.totalItems}`;
    queryParams += `&pageNumber=${transferData.pageNumber}`;
    queryParams += `&totalPages=${transferData.totalPages}`;
    queryParams += `&pageSize=${transferData.pageSize}`;

    if (transferData.orders.length) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `orders=${transferData.stringifyOrders()}`;
    }

    if (transferData.filters.length) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams = `filters=${transferData.stringifyFilters()}`;
    }

    return queryParams;
  }

  protected concatQueryParam(queryParams: string): string {
    return queryParams !== '' ? '&' : '';
  }
}
