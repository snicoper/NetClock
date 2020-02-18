import { Injectable } from '@angular/core';

import { SidebarMenuItemsModel } from '../models/sidebar-menu-items.model';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  menus: Array<any>;
  toggled = false;

  constructor() {
    const sidebar = this.loadFromLocalStorage();
    this.menus = sidebar ? sidebar : SidebarMenuItemsModel;
  }

  activeMenu(title: string): void {
    this.menus.forEach((menu) => {
      menu.active = menu.title === title;
    });

    this.saveToLocalStorage();
  }

  activeSubmenu(title: string): void {
    this.menus.forEach((menu) => {
      if (menu.type === 'dropdown') {
        menu.submenus.forEach((submenu) => {
          submenu.active = submenu.title === title;
        });
      }
    });

    this.saveToLocalStorage();
  }

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

  private loadFromLocalStorage(): Array<any> {
    return JSON.parse(localStorage.getItem('sidebar'));
  }

  private saveToLocalStorage(): void {
    localStorage.setItem('sidebar', JSON.stringify(this.menus));
  }
}
