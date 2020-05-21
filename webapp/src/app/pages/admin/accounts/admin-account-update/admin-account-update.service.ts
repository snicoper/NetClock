import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiUrls } from '../../../../core';
import { ApiRestBaseService, DebugService, SettingsService } from '../../../../services';
import { AdminAccountUpdateResultModel } from './admin-account-update-result.model';
import { AdminAccountUpdateModel } from './admin-account-update.model';

@Injectable()
export class AdminAccountUpdateService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  update(model: AdminAccountUpdateModel): Observable<AdminAccountUpdateResultModel> {
    const url = `${this.baseUrl}/update`;

    return this.http.put<AdminAccountUpdateResultModel>(url, model);
  }
}
