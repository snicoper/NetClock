import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { baseUrl } from '../../../../config';
import { RequestData } from '../../../../models';
import { ApiBaseService } from '../../../../services/api-base.service';
import { AdminUserDetailsModel, AdminUserListModel } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AdminAccountsService extends ApiBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${baseUrl}/admin/accounts`;
  }

  /** Obtener lista de usuarios. */
  public getUsers(requestData: RequestData<AdminUserListModel>): Observable<RequestData<AdminUserListModel>> {
    const url = `${this.baseUrl}?${this.prepareQueryParams<AdminUserListModel>(requestData)}`;

    return this.http.get<RequestData<AdminUserListModel>>(url);
  }

  /** Obtener un usuario por su slug. */
  public getUserByUserName(slug: string): Observable<AdminUserDetailsModel> {
    const url = `${this.baseUrl}/${slug}`;

    return this.http.get<AdminUserDetailsModel>(url);
  }
}
