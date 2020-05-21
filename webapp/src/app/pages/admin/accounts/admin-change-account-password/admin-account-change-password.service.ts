import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiUrls } from '../../../../core';
import { ApiRestBaseService, DebugService, SettingsService } from '../../../../services';
import { AdminAccountChangePasswordModel } from './admin-account-changepassword.model';

@Injectable()
export class AdminAccountChangePasswordService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  change(model: AdminAccountChangePasswordModel): Observable<void> {
    const url = `${this.baseUrl}/change-password`;

    return this.http.post<void>(url, model);
  }
}
