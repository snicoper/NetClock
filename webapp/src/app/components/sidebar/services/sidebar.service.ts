import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  toggled = false;
  menus = [
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
