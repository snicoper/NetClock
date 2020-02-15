import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Observable } from 'rxjs';

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

  getAllPaginated<TModel>(transferData: HttpTransferData<TModel>): Observable<HttpTransferData<TModel>> {
    const url = `${this.baseUrl}?${this.prepareQueryParams(transferData)}`;

    return this.http.get<HttpTransferData<TModel>>(url);
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

  protected prepareQueryParams<TModel>(transferData: HttpTransferData<TModel>): string {
    transferData = transferData ? transferData : new HttpTransferData<TModel>();

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
