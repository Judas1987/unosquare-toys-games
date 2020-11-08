import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.sass']
})
export class ProductListComponent implements OnInit {

  constructor(private route: Router) {
  }

  productList: string[] = [];

  ngOnInit(): void {

    this.productList.push('Product one');
    this.productList.push('Product two');
    this.productList.push('Product three');
    this.productList.push('Product four');
    this.productList.push('Product five');
  }

  addNewProduct(): void {
    this.route.navigate(['products/add'])
      .then(value => {
      })
      .catch(error => {
      });
  }

}
