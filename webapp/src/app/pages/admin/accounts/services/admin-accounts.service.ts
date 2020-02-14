import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseUrl } from '../../../../config';
import { HttpTransferData } from '../../../../models';
import { ApiBaseService } from '../../../../services/api-base.service';
import { AdminUserDetailsModel, AdminUserListModel } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AdminAccountsService extends ApiBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${BaseUrl}/admin/accounts`;
  }

  /** Obtener lista de usuarios. */
  getUsers(transferData: HttpTransferData<AdminUserListModel>): Observable<HttpTransferData<AdminUserListModel>> {
    const url = `${this.baseUrl}?${this.prepareQueryParams<AdminUserListModel>(transferData)}`;

    return this.http.get<HttpTransferData<AdminUserListModel>>(url);
  }

  /** Obtener un usuario por slug. */
  getUserBySlug(slug: string): Observable<AdminUserDetailsModel> {
    const url = `${this.baseUrl}/${slug}`;

    return this.http.get<AdminUserDetailsModel>(url);
  }
}
