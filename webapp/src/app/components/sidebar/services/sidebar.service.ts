import { Injectable } from '@angular/core';

import { SidebarMenuItemsModel } from '../models/sidebar-menu-items.model';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  menus = SidebarMenuItemsModel;
  toggled = false;

  toggle(): void {
    this.toggled = !this.toggled;
  }

  getSidebarState(): boolean {
    return this.toggled;
  }

  setSidebarState(state: boolean): void {
    this.toggled = state;
  }

  getMenuList(): any[] {
    return this.menus;
  }
}
