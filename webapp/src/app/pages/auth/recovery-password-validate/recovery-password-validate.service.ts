import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { appConfig } from '../../../app.config';
import { ApiUrls } from '../../../core/common';
import { ApiRestBaseService } from '../../../core/services';
import { RecoveryPasswordValidateModel } from './recovery-password-validate.model';

@Injectable()
export class RecoveryPasswordValidateService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${appConfig.baseApiUrl}/${ApiUrls.auth}`;
  }

  recoveryPasswordValidate(recoveryPasswordValidateModel: RecoveryPasswordValidateModel): Observable<void> {
    const url = `${this.baseUrl}/recovery-password/validate`;

    return this.http.post<void>(url, recoveryPasswordValidateModel);
  }
}
