import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiResult } from '../../models';
import { DebugService } from '../debug.service';

export abstract class ApiRestBaseService implements OnInit {
  protected baseUrl: string;

  protected constructor(
    protected http: HttpClient,
    protected debugService: DebugService
  ) {
  }

  ngOnInit(): void {
    if (!this.baseUrl) {
      this.debugService.raiseError('baseUrl no puede estar vacío.');
    }
  }

  getAllPaginated<TModel>(transferData: ApiResult<TModel>): Observable<ApiResult<TModel>> {
    const url = `${this.baseUrl}?${this.prepareQueryParams(transferData)}`;

    return this.http.get<ApiResult<TModel>>(url);
  }

  getBy<TModel>(param: any): Observable<TModel> {
    const url = `${this.baseUrl}/${param}`;

    return this.http.get<TModel>(url);
  }

  create<TModel>(model: TModel): Observable<TModel> {
    return this.http.post<TModel>(this.baseUrl, model);
  }

  update<TModel>(id: number | string, model: TModel): Observable<TModel> {
    const url = `${this.baseUrl}/${id}`;

    return this.http.put<TModel>(url, model);
  }

  delete<TModel>(id: number | string, model: TModel): Observable<void> {
    const url = `${this.baseUrl}/${id}`;

    return this.http.delete<void>(url, model);
  }

  protected prepareQueryParams<TModel>(transferData: ApiResult<TModel>): string {
    transferData = transferData ? transferData : new ApiResult<TModel>();

    return this.transferDataToQueryParams(transferData);
  }

  protected transferDataToQueryParams<TModel>(transferData: ApiResult<TModel>): string {
    let queryParams = '';
    queryParams += `totalItems=${transferData.totalItems}`;
    queryParams += `&pageNumber=${transferData.pageNumber}`;
    queryParams += `&totalPages=${transferData.totalPages}`;
    queryParams += `&pageSize=${transferData.pageSize}`;

    if (transferData.orders.length) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `orders=${JSON.stringify(transferData.orders)}`;
    }

    if (transferData.filters.length) {
      queryParams += this.concatQueryParam(queryParams);
      queryParams += `filters=${JSON.stringify(transferData.filters)}`;
    }

    return queryParams;
  }

  protected concatQueryParam(queryParams: string): string {
    return queryParams !== '' ? '&' : '';
  }
}
