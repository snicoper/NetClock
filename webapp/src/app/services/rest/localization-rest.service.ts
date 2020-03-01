import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { DebugService } from '../debug.service';
import { SettingsService } from '../settings.service';
import { ApiRestBaseService } from './api-rest-base.service';

@Injectable({
  providedIn: 'root'
})
export class LocalizationRestService extends ApiRestBaseService {
  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/localization`;
  }

  setCulture(culture: string): Observable<void> {
    return this.http.post<void>(this.baseUrl, { culture });
  }
}
