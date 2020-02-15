import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BaseUrl } from '../../../../config';
import { ApiBaseService } from '../../../../services/api-base.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAccountsService extends ApiBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${BaseUrl}/admin/accounts`;
  }
}
