import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';

import { DebugConsole } from '../core';
import { LogicalOperator, RelationalOperator, HttpTransferData } from '../models';

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
    // TODO: Test filter, delete.
    transferData.addFilter('userName', RelationalOperator.contains, 'ad');
    transferData.addFilter('userName', RelationalOperator.contains, 'o', LogicalOperator.or);

    let queryParams = '';
    queryParams += `totalItems=${transferData.totalItems}`;
    queryParams += `&pageNumber=${transferData.pageNumber}`;
    queryParams += `&totalPages=${transferData.totalPages}`;
    queryParams += `&pageSize=${transferData.pageSize}`;

    if (transferData.sorts) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `sorts=${transferData.sorts}`;
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
