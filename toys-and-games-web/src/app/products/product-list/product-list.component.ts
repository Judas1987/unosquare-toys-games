import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Product} from '../../models/Product';
import {ProductsService} from '../../Services/products.service';
import {BaseResponse} from '../../models/BaseResponse';
import {MatDialog} from '@angular/material/dialog';
import {DeleteConfirmationDialogComponent} from '../../delete-confirmation-dialog/delete-confirmation-dialog.component';
import {ProductEditComponent} from '../product-edit/product-edit.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.sass']
})
export class ProductListComponent implements OnInit {

  constructor(private route: Router, private productsService: ProductsService, public dialog: MatDialog) {
  }

  productList: Product[] = [];
  productId: string;

  ngOnInit(): void {
    this.loadProducts()
      .then(() => console.log('Products have been loaded successfully.'));
  }

  loadProducts(): Promise<void> {
    return new Promise<void>(((resolve, reject) => {
      this.productsService.getProducts()
        .subscribe((serviceResponse: BaseResponse<Product>) => {
          this.productList = serviceResponse.data;
          resolve();
        });
    }));
  }

  openProductRemovalDialog(productName: string, productId: string): any {
    this.productId = productId;

    const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent, {
      width: '500px',
      data: {name: productName, productId}
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      console.log(`The product id: ${this.productId} will be removed permanently.`);

      switch (result) {
        case true:
          this.deleteProduct(this.productId)
            .then(() => {
              this.loadProducts()
                .then(() => {
                  console.log('Product delete successfully.');
                  this.productId = '';
                });
            });
          break;
        case false:
          console.log('No product will be deleted.');
          break;
        default:
          console.log(`No action has been defined for ${result}`);
      }
      console.log(`Dialog result: ${result}`);
    });
  }

  deleteProduct(productId: string): Promise<void> {
    return new Promise<void>(((resolve, reject) => {
      this.productsService.deleteProduct(productId)
        .subscribe((results) => {
          resolve();
          console.log(results);
        });
    }));
  }

  openProductCreationDialog(): any {
    const dialogRef = this.dialog.open(ProductEditComponent, {
      width: '500px',
      data: {formMode: 'CREATE', productId: ''}
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result === true) {
        this.loadProducts()
          .then(() => {
          });
      }
    });
  }

  openProductUpdateDialog(productId: string): any {

    console.log(`This is the product id: ${productId}`);
    const dialogRef = this.dialog.open(ProductEditComponent, {
      width: '500px',
      data: {formMode: 'UPDATE', productId}
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result === true) {
        this.loadProducts()
          .then(() => {
          });
      }
    });
  }

  addNewProduct(): void {
    this.route.navigate(['products/add'])
      .then(value => {
      })
      .catch(error => {
      });
  }

}
