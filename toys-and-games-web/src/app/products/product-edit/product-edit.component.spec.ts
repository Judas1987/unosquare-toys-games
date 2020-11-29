import {ComponentFixture, TestBed} from '@angular/core/testing';
import {ProductEditComponent} from './product-edit.component';
import {CurrencyPipe} from '@angular/common';
import {ProductsService} from '../../Services/products.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {BaseResponse} from '../../models/BaseResponse';
import {Product} from '../../models/Product';
import {Observable} from 'rxjs';
import {Guid} from 'guid-typescript';
import {FormControl} from '@angular/forms';

describe('ProductEditComponent', () => {
    let component: ProductEditComponent;
    let fixture: ComponentFixture<ProductEditComponent>;
    let productServiceSpy: jasmine.SpyObj<ProductsService>;
    let matDialogSpy: jasmine.SpyObj<MatDialogRef<ProductEditComponent>>;

    beforeEach(async () => {
        const productServiceSpyFactory = jasmine.createSpyObj('ProductsService',
            ['getProductById', 'addProduct', 'updateProduct']);

        const matDialogSpyFactory = jasmine.createSpyObj('MatDialogRef',
            ['close']);

        await TestBed.configureTestingModule({
            providers: [
                CurrencyPipe,
                {
                    provide: ProductsService,
                    useValue: productServiceSpyFactory
                },
                {
                    provide: MAT_DIALOG_DATA, useValue: {
                        data: {
                            formMode: 'UPDATE',
                            productId: '12312'
                        }
                    }
                },
                {
                    provide: MatDialogRef,
                    useValue: matDialogSpyFactory
                }
            ],
            declarations: [ProductEditComponent]
        }).compileComponents();

        productServiceSpy = TestBed.inject(ProductsService) as jasmine.SpyObj<ProductsService>;
        matDialogSpy = TestBed.inject(MatDialogRef) as jasmine.SpyObj<MatDialogRef<ProductEditComponent>>;
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ProductEditComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create component instance', () => {
        expect(component).toBeTruthy();
    });

    it('should pass all product creation validations', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 234,
                company: 'Mattel',
                ageRestriction: 44,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();

    });

    it('should reject product name because length is greater than allowed', () => {

        component.productForm.patchValue(
            {
                name: 'Judas doll is such a nice doll to play with and I really like it. Never mind, I think this is not' +
                    ' going to pass',
                price: 234,
                company: 'Mattel',
                ageRestriction: 44,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeFalse();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product name because name is not provided', () => {
        component.productForm.patchValue(
            {
                name: '',
                price: 234,
                company: 'Mattel',
                ageRestriction: 44,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeFalse();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product price because is lower than one', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: -1,
                company: 'Mattel',
                ageRestriction: 44,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeFalse();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product price because is greater than one thousand', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 1001,
                company: 'Mattel',
                ageRestriction: 44,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeFalse();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product price because is missing', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: null,
                company: 'Mattel',
                ageRestriction: 44,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeFalse();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product company because is missing', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 134,
                company: null,
                ageRestriction: 44,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeFalse();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product company because length is greater than fifty', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 134,
                company: 'This is a very nice company that help the environment and things like that, I think everybody' +
                    ' should buy front this company.',
                ageRestriction: 44,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeFalse();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product age restriction because is lower than zero', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 134,
                company: 'Mattel',
                ageRestriction: -1,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeFalse();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product age restriction because is greater than 100', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 134,
                company: 'Mattel',
                ageRestriction: 101,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeFalse();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should accept product age restriction even when is missing', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 134,
                company: 'Mattel',
                ageRestriction: null,
                description: 'This is a test product description.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should reject product description because length is greater than one hundred', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 134,
                company: 'Mattel',
                ageRestriction: 0,
                description: 'This is a test product description which will not be allowed to be created because of' +
                    ' the description length. This is a cool form validation that is supported by the reactive form.'
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeFalse();
    });

    it('should accept product description even when is missing', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 134,
                company: 'Mattel',
                ageRestriction: 0,
                description: null
            });

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();
    });

    it('should execute getProductById once when in UPDATE mode', () => {

        const serviceResponse = new BaseResponse<Product>();
        serviceResponse.data = [new Product(Guid.parse('dca0d1ea-3618-4044-a79f-65af030cf595'), 'Judas doll',
            84, 'Mattel', 1, 'This is a very nice doll')];
        const serviceObservable = new Observable<BaseResponse<Product>>(observer => {
            observer.next(serviceResponse);
        });

        productServiceSpy.getProductById.and.returnValue(serviceObservable);
        component.data.formMode = 'UPDATE';

        component.ngOnInit();

        expect(productServiceSpy.getProductById.calls.count())
            .toBe(1, 'The number of calls to the getProductById method should be one.');

        expect(productServiceSpy.getProductById.calls.mostRecent().returnValue)
            .toBe(serviceObservable);
    });

    it('should not execute getProductById on INSERT mode', () => {

        const serviceResponse = new BaseResponse<Product>();
        serviceResponse.data = [new Product(Guid.parse('dca0d1ea-3618-4044-a79f-65af030cf595'), 'Judas doll',
            84, 'Mattel', 1, 'This is a very nice doll')];
        const serviceObservable = new Observable<BaseResponse<Product>>(observer => {
            observer.next(serviceResponse);
        });

        productServiceSpy.getProductById.and.returnValue(serviceObservable);
        component.data.formMode = 'INSERT';

        component.ngOnInit();

        expect(productServiceSpy.getProductById.calls.count())
            .toBe(0, 'The number of calls to the getProductById method should be zero.');
    });

    it('should reject form submission due to form being invalid', () => {
        component.productForm.patchValue(
            {
                name: null,
                price: '$134',
                company: 'Mattel',
                ageRestriction: 0,
                description: 'This is a nice description'
            });
        component.data.formMode = 'CREATE';

        expect(component.productForm.controls.name.valid).toBeFalse();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();

        component.onSubmit();

        expect(productServiceSpy.addProduct.calls.count())
            .toBe(0, 'The addProduct function was called even when it was not allowed.');

        expect(productServiceSpy.updateProduct.calls.count())
            .toBe(0, 'The updateProduct function was called even when it was not allowed.');
    });

    it('should successfully create a new product', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: '$134',
                company: 'Mattel',
                ageRestriction: 0,
                description: 'This is a nice description'
            });
        component.data.formMode = 'CREATE';

        const serviceObservable = new Observable<BaseResponse<Product>>(observer => {
            const serviceResponse = new BaseResponse<Product>();
            serviceResponse.data = [new Product(Guid.parse('442528ac-0668-43cd-a516-af4e79d41220'), 'Judas doll',
                84, 'Mattel', 1, 'This is a very nice doll')];
            observer.next(serviceResponse);
        });

        productServiceSpy.addProduct.and.returnValue(serviceObservable);

        expect(component.productForm.controls.name.valid).toBeTrue();
        expect(component.productForm.controls.price.valid).toBeTrue();
        expect(component.productForm.controls.company.valid).toBeTrue();
        expect(component.productForm.controls.ageRestriction.valid).toBeTrue();
        expect(component.productForm.controls.description.valid).toBeTrue();

        component.onSubmit();

        expect(productServiceSpy.addProduct.calls.count())
            .toBe(1, 'The addProduct function was called even when it was not allowed.');

        expect(productServiceSpy.updateProduct.calls.count())
            .toBe(0, 'The updateProduct function was called even when it was not allowed.');

        expect(matDialogSpy.close.calls.count())
            .toBe(1, 'The mat dialog close function was not called.');
    });

    it('should successfully update an existing product', () => {
        component.data.formMode = 'UPDATE';
        component.data.productId = '442528ac-0668-43cd-a516-af4e79d41220';

        const getProductByIdObservable = new Observable<BaseResponse<Product>>(observer => {
            const serviceResponse = new BaseResponse<Product>();
            serviceResponse.data = [new Product(Guid.parse('442528ac-0668-43cd-a516-af4e79d41220'), 'Judas doll',
                84, 'Mattel', 1, 'This is a very nice doll')];
            observer.next(serviceResponse);
        });

        productServiceSpy.getProductById.and.returnValue(getProductByIdObservable);

        const updateProductObservable = new Observable<BaseResponse<Product>>(observer => {
            const serviceResponse = new BaseResponse<Product>();
            serviceResponse.data = [new Product(Guid.parse('442528ac-0668-43cd-a516-af4e79d41220'), 'Judas doll',
                84, 'Mattel', 1, 'This is a very nice doll')];
            observer.next(serviceResponse);
        });

        productServiceSpy.updateProduct.and.returnValue(updateProductObservable);

        component.ngOnInit();
        component.onSubmit();

        expect(productServiceSpy.getProductById.calls.count())
            .toBe(1, 'The function getProductById was not called once.');

        expect(productServiceSpy.addProduct.calls.count())
            .toBe(0, 'The function addProduct was called during an update event.');

        expect(productServiceSpy.updateProduct.calls.count())
            .toBe(1, 'The function updateProduct was not called once.');

        expect(matDialogSpy.close.calls.count())
            .toBe(1, 'The mat dialog close function was not called.');
    });

    it('should transform price from numeric to currency', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: '134',
                company: 'Mattel',
                ageRestriction: 0,
                description: 'This is a nice description'
            });

        component.transformAmountToCurrency();

        const currentPrice = component.productForm.get('price') as FormControl;
        expect(currentPrice.value)
            .toBe('$134.00', 'The function transformAmountToCurrency did not transform the numeric value to currency.');
    });

    it('should empty the price form field when invalid number entered', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: 'dflkdjsnjka',
                company: 'Mattel',
                ageRestriction: 0,
                description: 'This is a nice description'
            });

        component.transformAmountToCurrency();

        const currentPrice = component.productForm.get('price') as FormControl;
        expect(currentPrice.value)
            .toBe(null, 'The function transformAmountToCurrency did not transform the numeric value to currency.');
    });

    it('should return zero in case of empty parameter', () => {
        const transformedValue = component.transformToNumber('');

        expect(transformedValue).toBe(0, 'The function transformToNumber did not return zero when empty parameter' +
            ' passed.');
    });

    it('should remove currency sign', () => {
        component.productForm.patchValue(
            {
                name: 'Judas doll',
                price: '$134',
                company: 'Mattel',
                ageRestriction: 0,
                description: 'This is a nice description'
            });

        component.transformToNumeric();

        const currentPrice = component.productForm.get('price') as FormControl;

        expect(currentPrice.value).toBe('134', 'The function transformToNumeric did not do its job correctly.');
    });
});

