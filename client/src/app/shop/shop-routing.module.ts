import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './product-details/product-details.component';

//for lazy loading
const routes: Routes = [  
  {path: '', component: ShopComponent}, //root component for shop module
  {path: ':id', component: ProductDetailsComponent, data: {breadcrumb: {alias: 'productDetails'}}},
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes) //not available in app module only in shop module
  ],
  exports: [RouterModule] //use this inside shop module
})
export class ShopRoutingModule { }
