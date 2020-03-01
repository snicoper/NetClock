import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { DebugService, SettingsService } from '../../../../services';
import { ApiRestBaseService } from '../../../../services/rest';

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
