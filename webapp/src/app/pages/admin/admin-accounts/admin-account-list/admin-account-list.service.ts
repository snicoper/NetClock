import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { appConfig } from '../../../../app.config';
import { ApiUrls } from '../../../../core/common';
import { ApiRestBaseService } from '../../../../core/services';

@Injectable()
export class AdminAccountListService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${appConfig.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }
}
