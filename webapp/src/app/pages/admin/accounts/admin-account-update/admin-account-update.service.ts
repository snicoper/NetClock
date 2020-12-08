import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AppConfig } from '../../../../app.config';
import { ApiUrls } from '../../../../core';
import { ApiRestBaseService } from '../../../../services';
import { AdminAccountUpdateResultModel } from './admin-account-update-result.model';
import { AdminAccountUpdateModel } from './admin-account-update.model';

@Injectable()
export class AdminAccountUpdateService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${AppConfig.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  update(model: AdminAccountUpdateModel): Observable<AdminAccountUpdateResultModel> {
    const url = `${this.baseUrl}/update/${model.id}`;

    return this.http.put<AdminAccountUpdateResultModel>(url, model);
  }
}
