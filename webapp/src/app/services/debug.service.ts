import { Injectable } from '@angular/core';

import { SettingsService } from './settings.service';

@Injectable({
  providedIn: 'root'
})
export class DebugService {
  constructor(private settingsService: SettingsService) {
  }

  errors(...errors: string[]): void {
    if (this.settingsService.isDebug === true) {
      // tslint:disable-next-line: no-console
      errors.forEach((error) => console.log(error));
    }
  }

  raiseError(message: string): void {
    if (this.settingsService.isDebug !== true) {
      message = '';
    }

    throw new Error(message);
  }
}
