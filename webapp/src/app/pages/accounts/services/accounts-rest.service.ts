import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { DebugService, SettingsService } from '../../../services';
import { ApiRestBaseService } from '../../../services/rest';
import { ChangePasswordModel } from '../change-password/change-password.model';

@Injectable({
  providedIn: 'root'
})
export class AccountsRestService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/accounts`;
  }

  /** Cambiar contraseña actual del usuario. */
  changePassword(changePasswordModel: ChangePasswordModel): Observable<void> {
    const url = `${this.baseUrl}/change-password`;

    return this.http.post<void>(url, changePasswordModel);
  }
}
