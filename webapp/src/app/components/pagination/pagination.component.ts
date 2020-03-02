import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';

import { ApiResult } from '../../models';

@Component({
  selector: 'nc-pagination',
  templateUrl: './pagination.component.html'
})
export class PaginationComponent<T> implements OnChanges {
  @Input() transferData: ApiResult<T>;
  @Input() itemsPageList = [10, 25, 50, 100];
  @Input() justifyContent = 'justify-content-end';
  /** Mostrar itemsPageList. */
  @Input() showPageList = true;
  /** Mostrar botones de paginas. */
  @Input() showPagesButtons = true;

  @Output() changePage = new EventEmitter<void>();
  @Output() changePageListNumber = new EventEmitter<void>();

  /** Primera pagina, por defecto 1. */
  firstPage: number;

  /** Última página. */
  lastPage: number;

  ngOnChanges(changes: SimpleChanges): void {
    const numMaxPages = this.transferData.ratio * 2 + 1;
    this.firstPage = 1;
    this.lastPage = this.transferData.totalPages > numMaxPages ? numMaxPages : this.transferData.totalPages;

    if (this.transferData.pageNumber > this.transferData.ratio + 1) {
      this.firstPage = this.transferData.pageNumber - this.transferData.ratio;

      if (this.transferData.totalPages > this.transferData.pageNumber + this.transferData.ratio) {
        this.lastPage = this.transferData.pageNumber + this.transferData.ratio;
      } else {
        this.lastPage = this.transferData.totalPages;
      }
    }
  }

  onChangePage(page: number): void {
    this.transferData.pageNumber = page;
    this.changePage.emit();
  }

  onChangePageListNumber(num: number): void {
    this.transferData.pageNumber = 1;
    this.transferData.pageSize = num;
    this.changePageListNumber.emit();
  }

  pageRange(): number[] {
    const pages = [];
    for (let i = this.firstPage; i <= this.lastPage; i += 1) {
      pages.push(i);
    }

    return pages;
  }
}
