import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiUrls } from '../../../core';
import { ApiRestBaseService, DebugService, SettingsService } from '../../../services';
import { RecoveryPasswordModel } from './recovery-password.model';

@Injectable()
export class RecoveryPasswordService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.auth}`;
  }

  recoveryPassword(recoveryPasswordModel: RecoveryPasswordModel): Observable<void> {
    const url = `${this.baseUrl}/recovery-password`;

    return this.http.post<void>(url, recoveryPasswordModel);
  }
}
