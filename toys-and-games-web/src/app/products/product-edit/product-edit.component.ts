import {Component, OnInit} from '@angular/core';
import {FormGroup, NgForm, FormControl, Validators} from '@angular/forms';
import {CurrencyPipe} from '@angular/common';
import {isNull} from 'util';


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

  constructor(private currencyPipe: CurrencyPipe) {
  }

  ngOnInit(): void {
  }


  onSubmit(): any {

    console.log(this.productForm);


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
