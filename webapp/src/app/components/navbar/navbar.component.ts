import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

import { UrlsApp } from '../../config';
import { CurrentUserModel } from '../../pages/auth/models';
import { AuthService } from '../../pages/auth/services/auth.service';
import { SidebarService } from '../sidebar/services/sidebar.service';

@Component({
  selector: 'nc-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit, OnDestroy {
  currentUser: CurrentUserModel;
  urlsApp = UrlsApp;

  private destroy$ = new Subject();

  constructor(
    public authService: AuthService,
    public sidebarService: SidebarService
  ) {
  }

  ngOnInit(): void {
    this.currentUser = this.authService.currentUserValue;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  toggle(): void {
    this.sidebarService.toggle();
  }
}
