import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseApiUrl } from '../../../config';
import { ApiBaseService } from '../../../services/api-base.service';
import { ChangePasswordModel } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AccountsService extends ApiBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${BaseApiUrl}/accounts`;
  }

  /** Cambiar contraseña actual del usuario. */
  changePassword(changePasswordModel: ChangePasswordModel): Observable<void> {
    const url = `${this.baseUrl}/change-password`;

    return this.http.post<void>(url, changePasswordModel);
  }
}
