import { DOCUMENT } from '@angular/common';
import { Inject, Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { LocalizationRestService } from './rest';

@Injectable({
  providedIn: 'root'
})
export class LocalizationService implements OnDestroy {
  private readonly defaultCulture = 'es';
  private readonly supportedCultures = ['es', 'ca', 'en'];

  private currentCulture$: Observable<string>;
  private currentCultureSubject$: BehaviorSubject<string>;
  private destroy$ = new Subject<void>();

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private localizationRestService: LocalizationRestService
  ) {
    const culture = localStorage.getItem('culture');
    if (culture) {
      this.currentCultureSubject$ = new BehaviorSubject<string>(culture);
    } else {
      this.currentCultureSubject$ = new BehaviorSubject<string>(this.defaultCulture);
    }

    this.currentCulture$ = this.currentCultureSubject$.asObservable().pipe(takeUntil(this.destroy$));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  initialize(): void {
    this.setCulture(this.getCurrentCultureValue());
  }

  setCulture(culture: string): void {
    if (this.supportedCultures.indexOf(culture) < 0) {
      throw new Error(`Culture ${culture} no soportada`);
    }

    this.currentCultureSubject$.next(culture);
    this.localizationRestService.setCulture(this.getCurrentCultureValue());
    this.document.documentElement.lang = this.getCurrentCultureValue();
    localStorage.setItem('culture', culture);
  }

  getCurrentCultureValue(): string {
    return this.currentCultureSubject$.value;
  }
}
