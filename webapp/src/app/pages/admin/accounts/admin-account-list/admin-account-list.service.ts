import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { AppConfig } from '../../../../app.config';
import { ApiUrls } from '../../../../core';
import { ApiRestBaseService } from '../../../../services';

@Injectable()
export class AdminAccountListService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${AppConfig.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }
}
