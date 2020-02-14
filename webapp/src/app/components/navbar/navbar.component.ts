import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { CurrentUserModel } from '../../pages/auth/models';
import { AuthService } from '../../pages/auth/services/auth.service';
import { SidebarService } from '../sidebar/services/sidebar.service';

@Component({
  selector: 'nc-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit, OnDestroy {
  currentUser: CurrentUserModel;

  private destroy$ = new Subject<void>();

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
