import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Product} from '../../models/Product';
import {Guid} from 'guid-typescript';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.sass']
})
export class ProductListComponent implements OnInit {

  constructor(private route: Router) {
  }

  productList: Product[] = [];

  ngOnInit(): void {

    this.productList.push(new Product(Guid.create(), 'Barbie Elsa - Frozen', 134, 'Mattel', 10, 'This is the best barbie in the world.'));
    this.productList.push(new Product(Guid.create(), 'Barbie Anna - Frozen', 134, 'Mattel', 10, 'This is the best barbie in the world.'));
    this.productList.push(new Product(Guid.create(), 'Goku action figure', 55, 'Mattel', 10, ''));
    this.productList.push(new Product(Guid.create(), 'Vegeta action figure', 55, 'Mattel', 10, ''));
    this.productList.push(new Product(Guid.create(), 'Winnie the poo', 12, 'Mattel', 10, ''));
  }

  addNewProduct(): void {
    this.route.navigate(['products/add'])
      .then(value => {
      })
      .catch(error => {
      });
  }

}
