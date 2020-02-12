export class RequestData<T> {
  public hasPreviousPage: boolean;
  public hasNextPage: boolean;
  public totalItems: number;
  public pageNumber: number;
  public totalPages: number;
  public pageSize: number;
  public ratio: number;
  public items: T[];
  public sorts: string;
  public filters: string;

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
