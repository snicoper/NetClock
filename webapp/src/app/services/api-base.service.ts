import { HttpClient } from '@angular/common/http';

import { RequestData } from '../models';

export abstract class ApiBaseService {
  protected baseUrl: string;

  protected constructor(protected http: HttpClient) {
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
      queryParams += queryParams !== '' ? '&' : '';
      queryParams += `sorts=${requestData.sorts}`;
    }

    return queryParams;
  }
}
