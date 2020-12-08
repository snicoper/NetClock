import { OrderType } from '../../../../models';

export const adminAccountListHeaderConfig = [
  {
    field: 'userName',
    text: 'Usuario',
    orderType: OrderType.none,
    sortable: true
  },
  {
    field: 'fullName',
    text: 'Nombre',
    sortable: false,
    orderType: OrderType.none
  },
  {
    field: 'email',
    text: 'Email',
    orderType: OrderType.none,
    sortable: true
  },
  {
    field: 'created',
    text: 'Fecha de registro',
    orderType: OrderType.none,
    sortable: true
  },
  {
    field: 'active',
    text: 'Activo',
    orderType: OrderType.none,
    sortable: true
  }
];
