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
    filterable: false,
    filterType: TableHeaderFilterType.none,
    sortable: false,
    orderType: OrderType.none
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
    field: 'created',
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
