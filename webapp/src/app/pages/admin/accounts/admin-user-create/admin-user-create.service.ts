import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiUrls } from '../../../../core';
import { DebugService, SettingsService } from '../../../../services';
import { ApiRestBaseService } from '../../../../services/rest';
import { AdminUserCreateResult } from './admin-user-create-result.model';
import { AdminUserCreateModel } from './admin-user-create.model';

@Injectable()
export class AdminUserCreateService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  create(model: AdminUserCreateModel): Observable<AdminUserCreateResult> {
    return this.http.post<AdminUserCreateResult>(this.baseUrl, model);
  }
}
