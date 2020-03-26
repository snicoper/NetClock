import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiUrls } from '../../../core';
import { DebugService, SettingsService } from '../../../services';
import { ApiRestBaseService } from '../../../services/rest';
import { ChangePasswordModel } from './change-password.model';

@Injectable()
export class ChangePasswordService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.accounts}`;
  }

  /** Cambiar contraseña actual del usuario. */
  change(changePasswordModel: ChangePasswordModel): Observable<void> {
    const url = `${this.baseUrl}/change-password`;

    return this.http.post<void>(url, changePasswordModel);
  }
}
