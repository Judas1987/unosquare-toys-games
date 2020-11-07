import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductItemComponent } from './products/product-item/product-item.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ProductEditComponent } from './products/product-edit/product-edit.component';
import { ProductsComponent } from './products/products.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CurrencyPipe} from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    ProductItemComponent,
    ProductListComponent,
    ProductEditComponent,
    ProductsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    CurrencyPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
