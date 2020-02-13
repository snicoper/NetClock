import { AdminUserListModel } from './admin-user-list.model';

export class AdminUserDetailsModel extends AdminUserListModel {
  id: string;
  firstName: string;
  lastName: string;
  updateAt: Date;
}
