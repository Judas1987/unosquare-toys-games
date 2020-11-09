import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Product} from '../../models/Product';
import {Guid} from 'guid-typescript';
import {ProductsService} from '../../Services/products.service';
import {BaseResponse} from '../../models/BaseResponse';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.sass']
})
export class ProductListComponent implements OnInit {

  constructor(private route: Router, private productsService: ProductsService) {
  }

  productList: Product[] = [];

  ngOnInit(): void {

    this.productsService.getProducts()
      .subscribe((serviceResponse: BaseResponse<Product>) => {
        this.productList = serviceResponse.data;
      });
  }

  addNewProduct(): void {
    this.route.navigate(['products/add'])
      .then(value => {
      })
      .catch(error => {
      });
  }

}
