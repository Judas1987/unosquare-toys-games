import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {ProductListComponent} from './products/product-list/product-list.component';
import {ProductEditComponent} from './products/product-edit/product-edit.component';
import {ProductsComponent} from './products/products.component';

const routes: Routes = [
  {
    path: '', redirectTo: '/products/list', pathMatch: 'full',
  },
  {
    path: 'products', redirectTo: '/products/list', pathMatch: 'full',
  },
  {
    path: 'products', component: ProductsComponent, children: [
      {
        path: ':id/edit', component: ProductEditComponent
      },
      {
        path: 'add', component: ProductEditComponent
      },
      {
        path: 'list', component: ProductListComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
