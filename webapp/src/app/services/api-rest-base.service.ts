import { HttpClient } from '@angular/common/http';
import { OnInit, Directive } from '@angular/core';
import { Observable } from 'rxjs';

import { RaiseError } from '../core';
import { ApiResult } from '../models';

@Directive()
export abstract class ApiRestBaseService implements OnInit {
  protected baseUrl: string;

  protected constructor(protected http: HttpClient) {
  }

  ngOnInit(): void {
    if (!this.baseUrl) {
      RaiseError('baseUrl no puede estar vacío.');
    }
  }

  getAllPaginated<TModel>(apiResult: ApiResult<TModel>): Observable<ApiResult<TModel>> {
    const url = `${this.baseUrl}?${this.prepareQueryParams(apiResult)}`;

    return this.http.get<ApiResult<TModel>>(url);
  }

  getBy<TModel>(param: any): Observable<TModel> {
    const url = `${this.baseUrl}/${param}`;

    return this.http.get<TModel>(url);
  }

  protected prepareQueryParams<TModel>(apiResult: ApiResult<TModel>): string {
    apiResult = apiResult ? apiResult : new ApiResult<TModel>();

    return this.apiResultToQueryParams(apiResult);
  }

  protected apiResultToQueryParams<TModel>(apiResult: ApiResult<TModel>): string {
    let queryParams = '';
    queryParams += `totalItems=${apiResult.totalItems}`;
    queryParams += `&pageNumber=${apiResult.pageNumber}`;
    queryParams += `&totalPages=${apiResult.totalPages}`;
    queryParams += `&pageSize=${apiResult.pageSize}`;

    if (apiResult.orders.length) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `orders=${JSON.stringify(apiResult.orders)}`;
    }

    if (apiResult.filters.length) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `filters=${JSON.stringify(apiResult.filters)}`;
    }

    return queryParams;
  }

  protected concatQueryParam(queryParams: string): string {
    return queryParams !== '' ? '&' : '';
  }
}
