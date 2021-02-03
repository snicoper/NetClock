import { Component } from '@angular/core';
import { LocalizationService } from './core/services';

@Component({
  selector: 'nc-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(
    private localizationService: LocalizationService
  ) {
    this.localizationService.initialize();
  }
}
