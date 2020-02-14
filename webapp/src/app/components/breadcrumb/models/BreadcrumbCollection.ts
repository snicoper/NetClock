import { BreadcrumbItem } from './breadcrumbItem';

export class BreadcrumbCollection {
  private readonly items: BreadcrumbItem[] = [];

  add(text: string, link: string, icon: string, activate?: boolean): BreadcrumbCollection {
    const breadcrumb = new BreadcrumbItem(text, link, icon, activate);
    this.items.push(breadcrumb);

    return this;
  }

  getItems(): BreadcrumbItem[] {
    return this.items;
  }

  hasItems(): boolean {
    return this.items.length > 0;
  }
}
