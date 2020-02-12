import { Component } from '@angular/core';
import { SidebarService } from '../components/sidebar/services/sidebar.service';

@Component({
  selector: 'nc-pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.scss']
})
export class PagesComponent {
  constructor(private sidebarService: SidebarService) {
  }

  public getSideBarState(): boolean {
    return this.sidebarService.getSidebarState();
  }
}
