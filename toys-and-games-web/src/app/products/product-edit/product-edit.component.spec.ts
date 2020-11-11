import {ComponentFixture, TestBed} from '@angular/core/testing';

import {ProductEditComponent} from './product-edit.component';
import {CurrencyPipe} from '@angular/common';
import {Router} from '@angular/router';
import {ProductsService} from '../../Services/products.service';
import {MatDialogRef} from '@angular/material/dialog';

describe('ProductEditComponent', () => {
    let component: ProductEditComponent;
    let fixture: ComponentFixture<ProductEditComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [CurrencyPipe, Router, ProductsService, MatDialogRef],
            declarations: [ProductEditComponent]
        })
            .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ProductEditComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
