import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../pages/auth/services/auth.service';

import { SidebarService } from './services/sidebar.service';

@Component({
  selector: 'nc-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  animations: [
    trigger('slide', [
      state('up', style({ height: 0 })),
      state('down', style({ height: '*' })),
      transition('up <=> down', animate(200))
    ])
  ]
})
export class SidebarComponent implements OnInit {
  public menus = [];

  constructor(
    public sidebarService: SidebarService,
    public authService: AuthService
  ) {
    this.menus = sidebarService.getMenuList();
   }

  public ngOnInit(): void {
  }

  getSideBarState(): boolean {
    return this.sidebarService.getSidebarState();
  }

  toggle(currentMenu): void {
    if (currentMenu.type === 'dropdown') {
      this.menus.forEach(element => {
        if (element === currentMenu) {
          currentMenu.active = !currentMenu.active;
        } else {
          element.active = false;
        }
      });
    }
  }

  getState(currentMenu): string {
    if (currentMenu.active) {
      return 'down';
    } else {
      return 'up';
    }
  }

  logout(): void {
    this.authService.logout();
  }
}
