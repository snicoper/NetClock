export class RequestData<T> {
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  totalItems: number;
  pageNumber: number;
  totalPages: number;
  pageSize: number;
  ratio: number;
  items: T[];
  sorts: string;
  filters: string;

  constructor() {
    // @see: webapi/src/Application/Models/Http/RequestData.cs default values.
    this.totalItems = 0;
    this.pageNumber = 1;
    this.totalPages = 1;
    this.pageSize = 10;
    this.ratio = 3;
    this.items = [];
    this.sorts = '';
    this.filters='';
  }
}
