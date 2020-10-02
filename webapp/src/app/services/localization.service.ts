import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import * as moment from 'moment';
import 'moment-timezone';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LocalizationService {
  private readonly defaultCulture = 'es-ES';
  private readonly supportedCultures = ['es-ES', 'ca-ES', 'en-GB'];

  private currentCultureSubject$ = new BehaviorSubject<string>(null);
  private timezoneSubject$ = new BehaviorSubject<string>(null);

  constructor(@Inject(DOCUMENT) private document: Document) {
  }

  /** Establecer valores por defecto de cultura y timezone. */
  initialize(): void {
    // Establecer cultura por defecto.
    const culture = localStorage.getItem('culture');
    if (culture) {
      this.currentCultureSubject$ = new BehaviorSubject<string>(culture);
    } else {
      this.setCulture(this.defaultCulture);
    }

    // Establecer timezone por defecto.
    const timezone = localStorage.getItem('timezone');
    if (timezone) {
      this.setTimezone(timezone);
    } else {
      this.setTimezone(Intl.DateTimeFormat().resolvedOptions().timeZone);
    }
  }

  /**
   * Establece la cultura por defecto al usuario.
   *
   * @param culture Cultura a establecer.
   */
  setCulture(culture: string): void {
    if (this.supportedCultures.indexOf(culture) < 0) {
      throw new Error(`Culture ${culture} no soportada.`);
    }

    localStorage.setItem('culture', culture);
    this.currentCultureSubject$.next(culture);
    this.document.documentElement.lang = culture;

    // Culturas soportadas por moment.
    // @see: https://stackoverflow.com/a/55827203
    const currentCulture = this.getCurrentCultureValue();
    const index = currentCulture.indexOf('-');
    const locale = index > 0 ? currentCulture.substr(0, index) : currentCulture;

    moment.locale(locale);
  }

  /** Obtener cultura actual. */
  getCurrentCultureValue(): string {
    return this.currentCultureSubject$.value;
  }

  /** Obtener observable de la cultura. */
  culture(): Observable<string> {
    return this.currentCultureSubject$.asObservable();
  }

  /**
   * Establecer el timezone y offset del usuario.
   *
   * @param timezone Timezone a establecer.
   */
  setTimezone(timezone: string): void {
    this.timezoneSubject$.next(timezone);
    moment.tz.setDefault(timezone);
    localStorage.setItem('timezone', this.getCurrentTimezoneValue());
  }

  /** Obtener timezone actual. */
  getCurrentTimezoneValue(): string {
    return this.timezoneSubject$.value;
  }

  /** Obtener observable del timezone. */
  timezone(): Observable<string> {
    return this.timezoneSubject$.asObservable();
  }

  /** Obtener el offset actual de moment. */
  utcOffset(): number {
    return moment().utcOffset();
  }
}
