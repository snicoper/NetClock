import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { ApiResult } from '../../core/models';

@Component({
  selector: 'nc-pagination',
  templateUrl: './pagination.component.html'
})
export class PaginationComponent<T> implements OnChanges {
  @Input() apiResult: ApiResult<T>;
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
    const numMaxPages = this.apiResult.ratio * 2 + 1;
    this.firstPage = 1;
    this.lastPage = this.apiResult.totalPages > numMaxPages ? numMaxPages : this.apiResult.totalPages;

    if (this.apiResult.pageNumber > this.apiResult.ratio + 1) {
      this.firstPage = this.apiResult.pageNumber - this.apiResult.ratio;

      if (this.apiResult.totalPages > this.apiResult.pageNumber + this.apiResult.ratio) {
        this.lastPage = this.apiResult.pageNumber + this.apiResult.ratio;
      } else {
        this.lastPage = this.apiResult.totalPages;
      }
    }
  }

  onChangePage(page: number): void {
    this.apiResult.pageNumber = page;
    this.changePage.emit();
  }

  onChangePageListNumber(num: number): void {
    this.apiResult.pageNumber = 1;
    this.apiResult.pageSize = num;
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
