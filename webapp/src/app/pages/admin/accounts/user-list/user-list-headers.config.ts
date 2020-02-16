import { TableHeaderFilterType } from '../../../../components/tables/table-header/core';
import { OrderType } from '../../../../models';

export const UserListHeaderConfig = [
  {
    field: 'userName',
    text: 'Usuario',
    filterable: true,
    filterType: TableHeaderFilterType.text,
    orderType: OrderType.none,
    sortable: true
  },
  {
    field: 'fullName',
    text: 'Nombre',
    sortable: false,
    orderType: OrderType.none,
    filterable: false,
    filterType: TableHeaderFilterType.none
  },
  {
    field: 'email',
    text: 'Email',
    filterable: true,
    filterType: TableHeaderFilterType.text,
    orderType: OrderType.none,
    sortable: true
  },
  {
    field: 'createAt',
    text: 'Fecha de registro',
    filterable: true,
    filterType: TableHeaderFilterType.dateRange,
    orderType: OrderType.none,
    sortable: true
  },
  {
    field: 'active',
    text: 'Activo',
    filterable: true,
    filterType: TableHeaderFilterType.boolean,
    orderType: OrderType.none,
    sortable: true
  }
];
