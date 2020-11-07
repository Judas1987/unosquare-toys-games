import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.sass']
})
export class ProductListComponent implements OnInit {

  constructor() {
  }

  productList: string[] = [];

  ngOnInit(): void {

    this.productList.push('Product one');
    this.productList.push('Product two');
    this.productList.push('Product three');
    this.productList.push('Product four');
    this.productList.push('Product five');
  }

}
