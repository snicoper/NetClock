import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { appConfig } from '../../../app.config';
import { ApiUrls } from '../../../core/common';
import { ApiRestBaseService } from '../../../core/services';
import { ChangePasswordModel } from './change-password.model';

@Injectable()
export class ChangePasswordService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${appConfig.baseApiUrl}/${ApiUrls.accounts}`;
  }

  /** Cambiar contraseña actual del usuario. */
  change(changePasswordModel: ChangePasswordModel): Observable<void> {
    const url = `${this.baseUrl}/change-password`;

    return this.http.post<void>(url, changePasswordModel);
  }
}
