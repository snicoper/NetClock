import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AppConfig } from '../../../../app.config';
import { ApiUrls } from '../../../../core';
import { ApiRestBaseService } from '../../../../services';
import { AdminAccountCreateResult } from './admin-account-create-result.model';
import { AdminAccountCreateModel } from './admin-account-create.model';

@Injectable()
export class AdminAccountCreateService extends ApiRestBaseService {
  constructor(protected http: HttpClient) {
    super(http);
    this.baseUrl = `${AppConfig.baseApiUrl}/${ApiUrls.adminAccounts}`;
  }

  create(model: AdminAccountCreateModel): Observable<AdminAccountCreateResult> {
    return this.http.post<AdminAccountCreateResult>(this.baseUrl, model);
  }
}
