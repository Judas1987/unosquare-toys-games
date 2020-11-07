import {Component, OnInit} from '@angular/core';
import {FormGroup, NgForm, FormControl, Validators} from '@angular/forms';
import {CurrencyPipe} from '@angular/common';


@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.sass']
})
export class ProductEditComponent implements OnInit {

  formattedAmount: string;

  productForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    price: new FormControl('', [Validators.min(0), Validators.max(100), Validators.required]),
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

  transformAmount(element): any {
    this.formattedAmount = this.currencyPipe.transform(this.formattedAmount, '$');

    console.log(this.formattedAmount);
  }
}
