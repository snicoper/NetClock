import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

import { urlsApp } from '../../config';
import { CurrentUserModel } from '../../pages/auth/models';
import { AuthService } from '../../pages/auth/services/auth.service';
import { SidebarService } from '../sidebar/services/sidebar.service';

@Component({
  selector: 'nc-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit, OnDestroy {
  public currentUser: CurrentUserModel;
  public urlsApp = urlsApp;

  private destroy$ = new Subject<void>();

  constructor(
    public authService: AuthService,
    public sidebarService: SidebarService
  ) {
  }

  public ngOnInit(): void {
    this.currentUser = this.authService.currentUserValue;
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public toggle(): void {
    this.sidebarService.toggle();
  }
}
