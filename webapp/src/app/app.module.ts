import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  PerfectScrollbarConfigInterface,
  PerfectScrollbarModule,
  PERFECT_SCROLLBAR_CONFIG
} from 'ngx-perfect-scrollbar';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentsModule } from './components/components.module';
import { GuardsModule } from './core/guards/guards.module';
import {
  ApiResultRequestInterceptor,
  ErrorRequestInterceptor,
  JwtResponseInterceptor,
  LocalizationResponseInterceptor
} from './core/interceptors';
import { Error404Component } from './pages/errors/error404/error404.component';
import { PagesComponent } from './pages/pages.component';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [AppComponent, PagesComponent, Error404Component],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    CommonModule,
    BrowserModule,
    ComponentsModule,
    FormsModule,
    GuardsModule,
    HttpClientModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    PerfectScrollbarModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtResponseInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorRequestInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ApiResultRequestInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LocalizationResponseInterceptor, multi: true },
    { provide: PERFECT_SCROLLBAR_CONFIG, useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
