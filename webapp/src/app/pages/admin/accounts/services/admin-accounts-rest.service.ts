import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BaseApiUrl } from '../../../../config';
import { ApiRestBaseService } from '../../../../services/api-rest-base.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAccountsRestService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${BaseApiUrl}/admin/accounts`;
  }
}
