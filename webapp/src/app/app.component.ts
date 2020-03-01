import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';

import { LocalizationRestService } from './services/rest';

@Component({
  selector: 'nc-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private languageService: LocalizationRestService
  ) {
  }

  ngOnInit(): void {
    // TODO: (PRUEBA) Cuando se implemente localización, importante cambiar el valor según localización.
    // @see: webapp/src/app/interceptors/culture.interceptor.ts.
    const defaultLanguage = 'es';
    this.document.documentElement.lang = defaultLanguage;
    this.languageService.setCulture(defaultLanguage)
      .subscribe(() => {
      });
  }
}
