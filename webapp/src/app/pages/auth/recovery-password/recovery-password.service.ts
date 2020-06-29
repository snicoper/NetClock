import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AppConfig } from '../../../app.config';
import { ApiUrls } from '../../../core';
import { ApiRestBaseService } from '../../../services';
import { RecoveryPasswordModel } from './recovery-password.model';

@Injectable()
export class RecoveryPasswordService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${AppConfig.baseApiUrl}/${ApiUrls.auth}`;
  }

  recoveryPassword(recoveryPasswordModel: RecoveryPasswordModel): Observable<void> {
    const url = `${this.baseUrl}/recovery-password`;

    return this.http.post<void>(url, recoveryPasswordModel);
  }
}
