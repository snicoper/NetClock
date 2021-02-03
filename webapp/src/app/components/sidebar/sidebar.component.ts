import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component } from '@angular/core';
import { appConfig } from '../../app.config';
import { AuthService } from '../../pages/auth/login/auth.service';
import { SidebarService } from './sidebar.service';

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
export class SidebarComponent {
  siteName = appConfig.siteName;
  menus = [];

  constructor(public sidebarService: SidebarService, public authService: AuthService) {
    this.menus = sidebarService.getMenuList();
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

        this.sidebarService.activeMenu(currentMenu.title);
      });
    }
  }

  getState(currentMenu): string {
    return currentMenu.active ? 'down' : 'up';
  }

  logout(): void {
    this.authService.logout();
  }
}
