import {Component, Inject, OnInit} from '@angular/core';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import {CurrencyPipe} from '@angular/common';
import {ProductsService} from '../../Services/products.service';
import {Product} from '../../models/Product';
import {Guid} from 'guid-typescript';
import {BaseResponse} from '../../models/BaseResponse';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';


@Component({
    selector: 'app-product-edit',
    templateUrl: './product-edit.component.html',
    styleUrls: ['./product-edit.component.sass']
})
export class ProductEditComponent implements OnInit {

    productForm = new FormGroup({
        name: new FormControl('',
            [Validators.required,
                Validators.maxLength(50)]),
        price: new FormControl('',
            [Validators.min(1),
                Validators.max(1000),
                Validators.required]),
        company: new FormControl('',
            [Validators.required,
                Validators.maxLength(50)]),
        ageRestriction: new FormControl('',
            [Validators.min(0),
                Validators.max(100)]),
        description: new FormControl('',
            [Validators.maxLength(100)])
    });

    /**
     * Product edit component constructor.
     * @param currencyPipe Represents the currency pipe instance to be injected to this component.
     * @param route Represents the router instance.
     * @param productsService Represents the product service to fetch data.
     * @param data Represents the data to be passed into this dialog.
     * @param dialogRef Represents the dialog reference instance to manipulate events from inside of the dialog.
     */
    constructor(private currencyPipe: CurrencyPipe,
                private productsService: ProductsService,
                @Inject(MAT_DIALOG_DATA) public data: { formMode: string, productId: string },
                public dialogRef: MatDialogRef<ProductEditComponent>) {
    }


    /**
     * Implementation of the OnInit life cycle event from angular.
     */
    ngOnInit(): void {
        if (this.data.formMode === 'UPDATE') {
            this.productsService.getProductById(this.data.productId)
                .subscribe((serviceResponse: BaseResponse<Product>) => {

                    let formattedPrice: string;
                    const convertedNumber = Number(serviceResponse.data[0].price);

                    if (typeof (convertedNumber) !== 'number' || convertedNumber <= 0) {
                        formattedPrice = '';
                    } else {
                        formattedPrice = this.currencyPipe.transform(convertedNumber, '$');
                    }

                    this.productForm.patchValue({
                        name: serviceResponse.data[0].name,
                        company: serviceResponse.data[0].company,
                        description: serviceResponse.data[0].description,
                        ageRestriction: serviceResponse.data[0].ageRestriction,
                        price: formattedPrice
                    });
                });
        }
    }


    /**
     * This function is executed every single time the user clicks on the save button.
     */
    onSubmit(): void {

        if (!this.productForm.valid) {
            return;
        }

        const name = this.productForm.get('name') as FormControl;
        const price = this.productForm.get('price') as FormControl;
        const company = this.productForm.get('company') as FormControl;
        const ageRestriction = this.productForm.get('ageRestriction') as FormControl;
        const description = this.productForm.get('description') as FormControl;

        const newProduct = new Product(Guid.create(), name.value, this.transformToNumber(price.value), company.value,
            ageRestriction.value, description.value);


        switch (this.data.formMode) {
            case 'CREATE':
                this.productsService.addProduct(newProduct)
                    .subscribe(() => {
                        this.dialogRef.close(true);
                    });
                break;
            case 'UPDATE':
                this.productsService.updateProduct(this.data.productId, newProduct)
                    .subscribe(() => {
                        this.dialogRef.close(true);
                    });
                break;
            default:
                console.log('This form can only manage INSERT and UPDATES.');
        }
    }

    /**
     * This function is executed to transform a simple number into a currency formatted string.
     */
    transformAmountToCurrency(): void {
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

    /**
     * This function takes the currency string and converts it to number, this is needed
     * to let the user update the price without having to worry about the currency sign and such.
     * @param value The number.
     */
    transformToNumber(value: string): number {
        if (value === '' || value === null) {
            return 0;
        }

        const convertedNumber = value.replace('$', '');

        return Number(convertedNumber);
    }

    /**
     * This function transforms a string into a number.
     */
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
