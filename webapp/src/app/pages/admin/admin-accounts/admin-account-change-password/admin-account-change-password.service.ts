import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { appConfig } from '../../../../app.config';
import { ApiUrls } from '../../../../core/common';
import { ApiRestBaseService } from '../../../../core/services';
import { AdminAccountChangePasswordModel } from './admin-account-change-password.model';

@Injectable()
export class AdminAccountChangePasswordService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${appConfig.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  change(model: AdminAccountChangePasswordModel): Observable<void> {
    const url = `${this.baseUrl}/change-password`;

    return this.http.post<void>(url, model);
  }
}
