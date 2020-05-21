import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ApiUrls } from '../../../../core';
import { ApiRestBaseService, DebugService, SettingsService } from '../../../../services';

@Injectable()
export class AdminAccountListService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }
}
