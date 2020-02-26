import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';

import { AuthRestService } from '../../pages/auth/services/auth-rest.service';
import { SettingsService } from '../../services';
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
  siteName: string;
  menus = [];

  constructor(
    public sidebarService: SidebarService,
    public authService: AuthRestService,
    private settingsService: SettingsService
  ) {
    this.siteName = this.settingsService.siteName;
    this.menus = sidebarService.getMenuList();
  }

  ngOnInit(): void {
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
    return currentMenu.active ? 'down': 'up';
  }

  logout(): void {
    this.authService.logout();
  }
}
