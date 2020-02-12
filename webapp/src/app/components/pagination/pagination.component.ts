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

  /** Emitir cuando cambia el numero de elementos a mostrar. */
  @Output() public changePageListNumber = new EventEmitter<RequestData<T>>();

  /** Emitir al cambiar de pagina. */
  @Output() public changePage = new EventEmitter<RequestData<T>>();

  public get pageRange(): number[] {
    const pages = [];
    for (let i = 1; i <= this.requestData.totalPages; i++) {
      pages.push(i);
    }

    return pages;
  }

  public onChangePage(page: number): void {
    this.requestData.pageNumber = page;
    this.changePage.emit(this.requestData);
  }

  public onChangePageListNumber(num: number): void {
    this.requestData.pageNumber = 1;
    this.requestData.pageSize = num;
    this.changePageListNumber.emit(this.requestData);
  }
}
