import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormGroup, NgForm, FormControl, Validators} from '@angular/forms';
import {CurrencyPipe} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {ProductsService} from '../../Services/products.service';
import {Product} from '../../models/Product';
import {Guid} from 'guid-typescript';
import {Subscription} from 'rxjs';
import {BaseResponse} from '../../models/BaseResponse';


@Component({
    selector: 'app-product-edit',
    templateUrl: './product-edit.component.html',
    styleUrls: ['./product-edit.component.sass']
})
export class ProductEditComponent implements OnInit, OnDestroy {

    private routeSub: Subscription;
    private formMode: string;
    private productId: string;

    productForm = new FormGroup({
        name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
        price: new FormControl('', [Validators.min(1), Validators.max(1000), Validators.required]),
        company: new FormControl('', [Validators.required, Validators.maxLength(50)]),
        ageRestriction: new FormControl('', [Validators.min(0), Validators.max(100)]),
        description: new FormControl('', [Validators.maxLength(100)])
    });

    constructor(private currencyPipe: CurrencyPipe, private route: Router,
                private productsService: ProductsService, private activatedRoute: ActivatedRoute) {
    }

    ngOnDestroy(): void {
        this.routeSub.unsubscribe();
    }

    ngOnInit(): void {
        this.routeSub = this.activatedRoute.params.subscribe(params => {

            if (params.productId === undefined || params.productId === null) {
                this.formMode = 'CREATE';
            } else {
                this.formMode = 'UPDATE';
                this.productId = params.productId;

                this.productsService.getProductById(params.productId)
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
        });
    }


    onSubmit(): any {

        const name = this.productForm.get('name') as FormControl;
        const price = this.productForm.get('price') as FormControl;
        const company = this.productForm.get('company') as FormControl;
        const ageRestriction = this.productForm.get('ageRestriction') as FormControl;
        const description = this.productForm.get('description') as FormControl;

        const newProduct = new Product(Guid.create(), name.value, this.transformToNumber(price.value), company.value,
            ageRestriction.value, description.value);


        switch (this.formMode) {
            case 'CREATE':
                this.productsService.addProduct(newProduct)
                    .subscribe((response: BaseResponse<Product>) => {
                        this.route.navigate(['/products/list'])
                            .then(value => {
                            })
                            .catch(error => {
                            });
                    });
                break;
            case 'UPDATE':
                this.productsService.updateProduct(this.productId, newProduct)
                    .subscribe((response: BaseResponse<Product>) => {
                        this.route.navigate(['/products/list'])
                            .then(value => {
                            })
                            .catch(error => {
                            });
                    });
                break;
            default:
                console.log('This form can only manage INSERT and UPDATES.');
        }


    }

    onDelete(productId): void {
        this.productsService.deleteProduct(productId)
            .subscribe(() => {

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

    transformToNumber(value: string): number {
        if (value === '' || value === null) {
            return 0;
        }

        const convertedNumber = value.replace('$', '');

        return Number(convertedNumber);
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
