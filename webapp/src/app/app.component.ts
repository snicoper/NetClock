import { DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';

import { LocalizationService } from './services';

@Component({
  selector: 'nc-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private localizationService: LocalizationService
  ) {
    this.localizationService.initialize();
  }
}
