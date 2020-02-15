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
  /** Mostrar solo botones de siguiente y previo. */
  @Input() small = false;

  /** Emitir al cambiar de pagina. */
  @Output() changePage = new EventEmitter<void>();
  /** Emitir cuando cambia el numero de elementos a mostrar. */
  @Output() changePageListNumber = new EventEmitter<void>();

  onChangePage(page: number): void {
    this.transferData.pageNumber = page;
    this.changePage.emit();
  }

  onChangePageListNumber(num: number): void {
    this.transferData.pageNumber = 1;
    this.transferData.pageSize = num;
    this.changePageListNumber.emit();
  }

  showPageList(): boolean {
    return !(this.transferData.pageSize >= this.itemsPageList[0]);
  }

  pageRange(): number[] {
    const pages = [];
    for (let i = 1; i <= this.transferData.totalPages; i += 1) {
      pages.push(i);
    }

    return pages;
  }
}
