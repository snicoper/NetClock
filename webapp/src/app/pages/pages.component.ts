import { Component, HostListener } from '@angular/core';

import { SidebarService } from '../components/sidebar/services/sidebar.service';

@Component({
  selector: 'nc-pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.scss']
})
export class PagesComponent {
  constructor(private sidebarService: SidebarService) {
  }

  // TODO: Mejorar el ocultar.
  @HostListener('window:resize', ['$event'])
  onResize(event) {
    const size = event.target.innerWidth;
    if (size && size < 768) {
      this.sidebarService.setSidebarState(true);
    }
  }

  getSideBarState(): boolean {
    return this.sidebarService.getSidebarState();
  }
}
