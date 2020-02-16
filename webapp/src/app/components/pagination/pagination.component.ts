import { Component, EventEmitter, Input, Output } from '@angular/core';

import { HttpTransferData } from '../../models';

@Component({
  selector: 'nc-pagination',
  templateUrl: './pagination.component.html'
})
export class PaginationComponent<T> {
  @Input() transferData: HttpTransferData<T>;
  @Input() justifyContent = 'justify-content-end';
  @Input() itemsPageList = [10, 25, 50, 100];
  /** Mostrar itemsPageList. */
  @Input() showPageList = true;
  /** Mostrar numero total de items. */
  @Input() showTotalItems = true;
  /** Mostrar botones de paginas. */
  @Input() showPagesButtons = true;

  @Output() changePage = new EventEmitter<void>();
  @Output() changePageListNumber = new EventEmitter<void>();

  randomId: string;

  constructor() {
    this.randomId = `page-list-${Math.random()}`;
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
    for (let i = 1; i <= this.transferData.totalPages; i += 1) {
      pages.push(i);
    }

    return pages;
  }
}
