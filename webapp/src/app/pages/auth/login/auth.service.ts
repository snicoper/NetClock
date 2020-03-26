import { HttpClient } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { ApiUrls, SiteUrls } from '../../../core';
import { ApiRestBaseService, DebugService, SettingsService } from '../../../services';
import { CurrentUserModel } from './current-user.model';
import { LoginModel } from './login.model';

@Injectable({ providedIn: 'root' })
export class AuthService extends ApiRestBaseService implements OnDestroy {
  readonly currentUser$: Observable<CurrentUserModel>;

  private readonly destroy$ = new Subject<void>();
  private readonly currentUserSubject: BehaviorSubject<CurrentUserModel>;

  constructor(
    protected http: HttpClient,
    protected debugService: DebugService,
    private settingsService: SettingsService,
    private router: Router
  ) {
    super(http, debugService);
    this.baseUrl = `${this.settingsService.baseApiUrl}/${ApiUrls.auth}`;
    this.currentUserSubject = new BehaviorSubject<CurrentUserModel>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser$ = this.currentUserSubject.asObservable().pipe(takeUntil(this.destroy$));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  get currentUserValue(): CurrentUserModel {
    return this.currentUserSubject.value;
  }

  hasRole(role: string): boolean {
    if (!this.currentUserValue) {
      return false;
    }

    const token = this.decodeToken(this.currentUserValue.token);
    const roles = token.role.split(',');

    return roles.includes(role);
  }

  login(loginModel: LoginModel): Observable<CurrentUserModel> {
    const url = `${this.baseUrl}/login`;

    return this.http.post<CurrentUserModel>(url, loginModel);
  }

  setCurrentUser(currentUser: CurrentUserModel): void {
    const data = this.decodeToken(currentUser.token);
    currentUser.expires = new Date(data.exp * 1000);
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
    this.currentUserSubject.next(currentUser);
  }

  logout(): void {
    const url = `${this.baseUrl}/logout`;

    this.http.post<void>(url, null).subscribe(
      () => {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
        this.router.navigate([SiteUrls.login]);
      }
    );
  }

  private decodeToken(token: string): any {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');

    return JSON.parse(window.atob(base64));
  }
}
