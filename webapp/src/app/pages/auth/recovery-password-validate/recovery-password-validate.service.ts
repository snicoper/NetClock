import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AppConfig } from '../../../app.config';
import { ApiUrls } from '../../../core';
import { ApiRestBaseService } from '../../../services';
import { RecoveryPasswordValidateModel } from './recovery-password-validate.model';

@Injectable()
export class RecoveryPasswordValidateService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${AppConfig.baseApiUrl}/${ApiUrls.auth}`;
  }

  recoveryPasswordValidate(recoveryPasswordValidateModel: RecoveryPasswordValidateModel): Observable<void> {
    const url = `${this.baseUrl}/recovery-password/validate`;

    return this.http.post<void>(url, recoveryPasswordValidateModel);
  }
}
