import { BreadcrumbItem } from './breadcrumbItem';

export class BreadcrumbCollection {
  private readonly items = Array<BreadcrumbItem>();

  public add(text: string, link: string, icon: string, activate?: boolean): BreadcrumbCollection {
    const breadcrumb = new BreadcrumbItem(text, link, icon, activate);
    this.items.push(breadcrumb);

    return this;
  }

  public getItems(): BreadcrumbItem[] {
    return this.items;
  }

  public hasItems(): boolean {
    return this.items.length > 0;
  }
}
