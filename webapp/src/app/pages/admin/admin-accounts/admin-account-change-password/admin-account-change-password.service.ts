import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AppConfig } from '../../../../app.config';
import { ApiUrls } from '../../../../core';
import { ApiRestBaseService } from '../../../../services';
import { AdminAccountChangePasswordModel } from './admin-account-change-password.model';

@Injectable()
export class AdminAccountChangePasswordService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${AppConfig.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  change(model: AdminAccountChangePasswordModel): Observable<void> {
    const url = `${this.baseUrl}/change-password`;

    return this.http.post<void>(url, model);
  }
}
