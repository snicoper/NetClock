import { DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, OnDestroy } from '@angular/core';
import * as moment from 'moment';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { AppConfig } from '../app.config';
import { ApiUrls } from '../core';
import { ApiRestBaseService } from './api-rest-base.service';

@Injectable({ providedIn: 'root' })
export class LocalizationService extends ApiRestBaseService implements OnDestroy {
  private readonly defaultCulture = 'es-ES';
  private readonly supportedCultures = ['es-ES', 'ca-ES', 'en-GB'];

  private currentCulture$: Observable<string>;
  private currentCultureSubject$: BehaviorSubject<string>;
  private destroy$ = new Subject<void>();

  constructor(
    @Inject(DOCUMENT) private document: Document,
    protected http: HttpClient
  ) {
    super(http);
    this.baseUrl = `${AppConfig.baseApiUrl}/${ApiUrls.localization}`;

    const culture = localStorage.getItem('culture');
    if (culture) {
      this.currentCultureSubject$ = new BehaviorSubject<string>(culture);
    } else {
      this.setCulture(this.defaultCulture);
      this.currentCultureSubject$ = new BehaviorSubject<string>(this.defaultCulture);
    }

    this.currentCulture$ = this.currentCultureSubject$.asObservable().pipe(takeUntil(this.destroy$));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  initialize(culture?: string): void {
    if (culture) {
      this.setCulture(culture);
    }
  }

  setCulture(culture: string): void {
    if (this.supportedCultures.indexOf(culture) < 0) {
      throw new Error(`Culture ${culture} no soportada.`);
    }

    localStorage.setItem('culture', culture);
    this.currentCultureSubject$.next(culture);
    this.http.post<void>(this.baseUrl, { culture });
    this.document.documentElement.lang = culture;
    this.configureMoment();
  }

  getCurrentCultureValue(): string {
    return this.currentCultureSubject$.value;
  }

  private configureMoment(): void {
    // @see: https://stackoverflow.com/a/55827203
    const currentCulture = this.getCurrentCultureValue();
    const index = currentCulture.indexOf('-');
    const locale = index > 0 ? currentCulture.substr(0, index) : currentCulture;

    moment.locale(locale);
  }
}
