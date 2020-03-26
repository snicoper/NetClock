import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiUrls } from '../../../../core';
import { ApiRestBaseService, DebugService, SettingsService } from '../../../../services';
import { AdminUserUpdateResultModel } from './admin-user-update-result.model';
import { AdminUserUpdateModel } from './admin-user-update.model';

@Injectable()
export class AdminUserUpdateService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  update(userId: string, model: AdminUserUpdateModel): Observable<AdminUserUpdateResultModel> {
    const url = `${this.baseUrl}/${userId}`;

    return this.http.put<AdminUserUpdateResultModel>(url, model);
  }
}
