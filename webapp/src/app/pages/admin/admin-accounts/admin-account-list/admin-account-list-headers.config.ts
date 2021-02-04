import { OrderTypes } from '../../../../core/models';

export const adminAccountListHeaderConfig = [
  {
    field: 'userName',
    text: 'Usuario',
    orderType: OrderTypes.none,
    sortable: true
  },
  {
    field: 'fullName',
    text: 'Nombre',
    sortable: false,
    orderType: OrderTypes.none
  },
  {
    field: 'email',
    text: 'Email',
    orderType: OrderTypes.none,
    sortable: true
  },
  {
    field: 'created',
    text: 'Fecha de registro',
    orderType: OrderTypes.none,
    sortable: true
  },
  {
    field: 'active',
    text: 'Activo',
    orderType: OrderTypes.none,
    sortable: true
  }
];
