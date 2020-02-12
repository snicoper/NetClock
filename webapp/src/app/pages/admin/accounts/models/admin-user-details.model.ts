import { AdminUserListModel } from './admin-user-list.model';

export class AdminUserDetailsModel extends AdminUserListModel {
  public id: string;
  public firstName: string;
  public lastName: string;
  public updateAt: Date;
}
