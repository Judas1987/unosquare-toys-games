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

  /**
   * Product list component constructor
   * @param route Represents the router instance.
   * @param productsService Represents the instance of the product service to manipulate data.
   * @param dialog Represents the dialog instance to open different dialogs within the application.
   */
  constructor(private route: Router, private productsService: ProductsService, public dialog: MatDialog) {
  }

  productList: Product[] = [];
  productId: string;

  /**
   * On init event lifecycle from angular.
   */
  ngOnInit(): void {
    this.loadProducts()
      .then(() => console.log('Products have been loaded successfully.'));
  }

  /**
   * This function loads all the products into the UI.
   */
  loadProducts(): Promise<void> {
    return new Promise<void>(((resolve, reject) => {
      this.productsService.getProducts()
        .subscribe((serviceResponse: BaseResponse<Product>) => {
          this.productList = serviceResponse.data;
          resolve();
        });
    }));
  }

  /**
   * This function opens the dialog to confirm the deletion of a given product.
   * @param productName Represents the product name.
   * @param productId Represents the product id.
   */
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

  /**
   * This function calls the product service to remove a given product.
   * @param productId Represents the product id of the product to be removed.
   */
  deleteProduct(productId: string): Promise<void> {
    return new Promise<void>(((resolve, reject) => {
      this.productsService.deleteProduct(productId)
        .subscribe((results) => {
          resolve();
          console.log(results);
        });
    }));
  }

  /**
   * This function open the product creation dialog and passes the necessary information to put the form in the mode.
   */
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

  /**
   * This function opens the product modification dialog and passes the necessary information to put the form in that mode.
   * @param productId Represents the product id.
   */
  openProductUpdateDialog(productId: string): any {
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
}
