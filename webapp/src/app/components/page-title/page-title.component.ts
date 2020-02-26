import { Component, Input, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';

import { SettingsService } from '../../services';

/** Establece el titulo de la pagina (pestaña del navegador). */
@Component({
  selector: 'nc-page-title',
  template: ''
})
export class PageTitleComponent implements OnInit {
  @Input() pageTitle: string;

  constructor(
    private route: ActivatedRoute,
    private title: Title,
    private settingsService: SettingsService
  ) {
  }

  ngOnInit(): void {
    if (this.pageTitle) {
      this.setTitle(this.pageTitle);
    } else if ('title' in this.route.snapshot.data) {
      this.setTitle(this.route.snapshot.data.title);
    } else {
      this.setTitle();
    }
  }

  private setTitle(pageTitle?: string) {
    let title = this.settingsService.siteName;
    if (pageTitle) {
      title = `${pageTitle} - ${title}`;
    }

    this.title.setTitle(title);
  }
}
