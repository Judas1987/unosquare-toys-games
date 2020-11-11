import {ComponentFixture, TestBed} from '@angular/core/testing';

import {ProductEditComponent} from './product-edit.component';
import {CurrencyPipe} from '@angular/common';
import {ProductsService} from '../../Services/products.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {RouterTestingModule} from '@angular/router/testing';
import {HttpClientTestingModule} from '@angular/common/http/testing';

describe('ProductEditComponent', () => {
  let component: ProductEditComponent;
  let fixture: ComponentFixture<ProductEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule],
      providers: [
        CurrencyPipe,
        ProductsService,
        {provide: MAT_DIALOG_DATA, useValue: {data: {formMode: 'INSERT', productId: '12312'}}},
        {provide: MatDialogRef, useValue: {data: {formMode: 'INSERT', productId: '12312'}}}
      ],
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
