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

  /** Observable de cultura. */
  get culture(): Observable<string> {
    return this.currentCultureSubject$.asObservable();
  }

  /** Observable del timezone. */
  get timezone(): Observable<string> {
    return this.timezoneSubject$.asObservable();
  }

  /** Establecer valores por defecto de cultura y timezone. */
  initialize(): void {
    // Establecer cultura por defecto.
    const culture = localStorage.getItem('culture') || this.defaultCulture;
    this.setCulture(culture);

    // Establecer timezone por defecto.
    const timezone = localStorage.getItem('timezone') || Intl.DateTimeFormat().resolvedOptions().timeZone;
    this.setTimezone(timezone);
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
    const index = culture.indexOf('-');
    const locale = index > 0 ? culture.substr(0, index) : culture;
    moment.locale(locale);
  }

  /** Obtener el valor de la cultura actual. */
  getCultureValue(): string {
    return this.currentCultureSubject$.value;
  }

  /**
   * Establecer el timezone y offset del usuario.
   *
   * @param timezone Timezone a establecer.
   */
  setTimezone(timezone: string): void {
    moment.tz.setDefault(timezone);
    this.timezoneSubject$.next(timezone);
    localStorage.setItem('timezone', timezone);
  }

  /** Obtener el valor timezone actual. */
  getTimezoneValue(): string {
    return this.timezoneSubject$.value;
  }

  /** Obtener el offset actual de moment. */
  utcOffset(): number {
    return moment().utcOffset();
  }
}
