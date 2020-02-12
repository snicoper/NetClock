export class BreadcrumbItem {
  public text: string;
  public link: string;
  public icon: string;
  public active: boolean;

  constructor(text: string, link: string, icon: string, activate?: boolean) {
    this.text = text;
    this.link = link;
    this.icon = icon;
    this.active = activate !== false;
  }
}
