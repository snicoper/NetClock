import { Component, EventEmitter, Input, Output } from '@angular/core';

import { RequestData } from '../../models';

@Component({
  selector: 'nc-pagination',
  templateUrl: './pagination.component.html'
})
export class PaginationComponent<T> {
  @Input() requestData: RequestData<T>;
  @Input() justifyContent = 'justify-content-end';
  @Input() itemsPageList = [10, 25, 50, 100];
  /** Mostrar solo botones de siguiente y previo. */
  @Input() small = false;

  /** Emitir al cambiar de pagina. */
  @Output() changePage = new EventEmitter<RequestData<T>>();
  /** Emitir cuando cambia el numero de elementos a mostrar. */
  @Output() changePageListNumber = new EventEmitter<RequestData<T>>();

  onChangePage(page: number): void {
    this.requestData.pageNumber = page;
    this.changePage.emit(this.requestData);
  }

  onChangePageListNumber(num: number): void {
    this.requestData.pageNumber = 1;
    this.requestData.pageSize = num;
    this.changePageListNumber.emit(this.requestData);
  }

  showPageList(): boolean {
    return !(this.requestData.pageSize >= this.itemsPageList[0]);
  }

  pageRange(): number[] {
    const pages = [];
    for (let i = 1; i <= this.requestData.totalPages; i++) {
      pages.push(i);
    }

    return pages;
  }
}
