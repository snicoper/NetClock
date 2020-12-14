import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { appConfig } from '../../../../app.config';
import { ApiUrls } from '../../../../core/common';
import { ApiRestBaseService } from '../../../../core/services';
import { AdminAccountUpdateResultModel } from './admin-account-update-result.model';
import { AdminAccountUpdateModel } from './admin-account-update.model';

@Injectable()
export class AdminAccountUpdateService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${appConfig.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  update(model: AdminAccountUpdateModel): Observable<AdminAccountUpdateResultModel> {
    const url = `${this.baseUrl}/update/${model.id}`;

    return this.http.put<AdminAccountUpdateResultModel>(url, model);
  }
}
