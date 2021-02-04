import { Injectable } from '@angular/core';
import { sidebarMenuItemsModel } from './sidebar-menu-items.model';

@Injectable({ providedIn: 'root' })
export class SidebarService {
  menus: any[];
  toggled = false;

  constructor() {
    const sidebar = this.loadFromLocalStorage();
    this.menus = sidebar ? sidebar : sidebarMenuItemsModel;
  }

  activeMenu(title: string): void {
    this.menus.forEach((menu) => {
      if (menu.title === title) {
        menu.active = !menu.active;
      } else {
        menu.active = false;
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

  private loadFromLocalStorage(): any[] {
    return JSON.parse(localStorage.getItem('sidebar'));
  }

  private saveToLocalStorage(): void {
    localStorage.setItem('sidebar', JSON.stringify(this.menus));
  }
}
