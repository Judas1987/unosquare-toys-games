import {Injectable} from '@angular/core';
import {Product} from '../models/Product';
import {Guid} from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  products: Product[] = [];
  addedProducts: Product[] = [];

  getProducts(): Product[] {

    this.products = [];

    this.products.push(new Product(Guid.create(), 'Barbie Elsa - Frozen', 134, 'Mattel', 10, 'This is the best barbie in the world.'));
    this.products.push(new Product(Guid.create(), 'Barbie Anna - Frozen', 134, 'Mattel', 10, 'This is the best barbie in the world.'));
    this.products.push(new Product(Guid.create(), 'Goku action figure', 55, 'Mattel', 10, ''));
    this.products.push(new Product(Guid.create(), 'Vegeta action figure', 55, 'Mattel', 10, ''));
    this.products.push(new Product(Guid.create(), 'Winnie the poo', 12, 'Mattel', 10, ''));

    this.products.push.apply(this.products, this.addedProducts);

    return this.products;
  }

  addProduct(newProduct: Product): void {
    this.addedProducts.push(newProduct);
  }

  constructor() {
  }
}
