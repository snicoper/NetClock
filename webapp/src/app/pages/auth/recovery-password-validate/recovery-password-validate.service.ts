import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiUrls } from '../../../core';
import { ApiRestBaseService, DebugService, SettingsService } from '../../../services';
import { RecoveryPasswordValidateModel } from './recovery-password-validate.model';

@Injectable()
export class RecoveryPasswordValidateService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.auth}`;
  }

  recoveryPasswordValidate(recoveryPasswordValidateModel: RecoveryPasswordValidateModel): Observable<void> {
    const url = `${this.baseUrl}/recovery-password/validate`;

    return this.http.post<void>(url, recoveryPasswordValidateModel);
  }
}
