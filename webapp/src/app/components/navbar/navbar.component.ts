import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { CurrentUserModel } from '../../pages/auth/models';
import { AuthRestService } from '../../pages/auth/services/auth-rest.service';
import { SidebarService } from '../sidebar/services/sidebar.service';

@Component({
  selector: 'nc-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit, OnDestroy {
  currentUser: CurrentUserModel;

  private destroy$ = new Subject<void>();

  constructor(
    public authService: AuthRestService,
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
