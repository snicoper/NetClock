import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Error403Component } from './error403/error403.component';

// http://example.com/errors
const routes: Routes = [
  {
    path: '403',
    component: Error403Component,
    data: { title: 'Sin autorización' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ErrorsRoutingModule {}
