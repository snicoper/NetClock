import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ApiRestBaseService, DebugService, SettingsService } from '../../../../services';

@Injectable({
  providedIn: 'root'
})
export class AdminAccountsRestService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/admin/accounts`;
  }
}
