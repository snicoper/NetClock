import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  siteName = 'Net clock';
  apiUrl: string;
  baseApiUrl: string;
  siteUrl: string;
  isDebug: boolean;

  constructor() {
    this.apiUrl = environment.apiUrl;
    this.siteUrl = environment.siteUrl;
    this.isDebug = environment.isDebug;
    this.baseApiUrl = `${this.apiUrl}/${environment.apiSegment}`;
  }
}
