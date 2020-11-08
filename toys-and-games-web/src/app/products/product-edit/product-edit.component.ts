import {Component, OnInit} from '@angular/core';
import {FormGroup, NgForm, FormControl, Validators} from '@angular/forms';
import {CurrencyPipe} from '@angular/common';
import {Router} from '@angular/router';
import {ProductsService} from '../../Services/products.service';
import {Product} from '../../models/Product';
import {Guid} from 'guid-typescript';


@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.sass']
})
export class ProductEditComponent implements OnInit {

  formattedAmount: string;

  productForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    price: new FormControl('', [Validators.min(1), Validators.max(1000), Validators.required]),
    company: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    ageRestriction: new FormControl('', [Validators.min(0), Validators.max(100)]),
    description: new FormControl('', [Validators.maxLength(100)])
  });

  constructor(private currencyPipe: CurrencyPipe, private route: Router, private productsService: ProductsService) {
  }

  ngOnInit(): void {
  }


  onSubmit(): any {

    const name = this.productForm.get('name') as FormControl;
    const price = this.productForm.get('price') as FormControl;
    const company = this.productForm.get('company') as FormControl;
    const ageRestriction = this.productForm.get('ageRestriction') as FormControl;
    const description = this.productForm.get('description') as FormControl;

    const newProduct = new Product(Guid.create(), name.value, price.value, company.value, ageRestriction.value, description.value);

    this.productsService.addProduct(newProduct);
    
    this.route.navigate(['/products/list'])
      .then(value => {
      })
      .catch(error => {
      });
  }

  cancelAction(): void {
    this.route.navigate(['/products/list'])
      .then(value => {
      })
      .catch(error => {
      });
  }

  transformAmount(): void {
    const currentPrice = this.productForm.get('price') as FormControl;
    let formattedPrice: string;
    const convertedNumber = Number(currentPrice.value);

    if (typeof (convertedNumber) !== 'number' || convertedNumber <= 0) {
      formattedPrice = '';
    } else {
      formattedPrice = this.currencyPipe.transform(convertedNumber, '$');
    }

    this.productForm.patchValue({
      price: formattedPrice
    });
  }

  transformToNumeric(): void {
    const currentPrice = this.productForm.get('price') as FormControl;

    if (currentPrice.value === '' || currentPrice.value === null) {
      return;
    }

    const price = currentPrice.value.replace('$', '');
    this.productForm.patchValue({
      price
    });
  }

}
