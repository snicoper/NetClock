import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  public toggled = false;
  public menus = [
    {
      title: '- Administración',
      type: 'header'
    },
    {
      title: 'Administración',
      icon: 'fas fa-user-shield',
      active: false,
      type: 'dropdown',
      // badge: {
      //   text: 'New ',
      //   class: 'badge-warning'
      // },
      submenus: [
        {
          title: 'Admin',
          link: '/admin'
          // badge: {
          //   text: 'Pro ',
          //   class: 'badge-success'
          // }
        },
        {
          title: 'Usuarios',
          link: '/admin/accounts'
        }
      ]
    },
    {
      title: '- Usuarios',
      type: 'header'
    },
    {
      title: 'Usuario',
      icon: 'fas fa-user',
      active: false,
      type: 'dropdown',
      // badge: {
      //   text: '3',
      //   class: 'badge-danger'
      // },
      submenus: [
        {
          title: 'Perfil',
          link: '/accounts/profile'
        },
        {
          title: 'Cambiar contraseña',
          link: '/accounts/change-password'
        }
      ]
    },
    // {
    //   title: 'Extra',
    //   type: 'header'
    // },
    // {
    //   title: 'Documentation',
    //   icon: 'fa fa-book',
    //   active: false,
    //   type: 'simple',
    //   badge: {
    //     text: 'Beta',
    //     class: 'badge-primary'
    //   },
    // },
  ];

  constructor() {
  }

  public toggle(): void {
    this.toggled = !this.toggled;
  }

  public getSidebarState(): boolean {
    return this.toggled;
  }

  public setSidebarState(state: boolean): void {
    this.toggled = state;
  }

  public getMenuList(): Array<any> {
    return this.menus;
  }
}
