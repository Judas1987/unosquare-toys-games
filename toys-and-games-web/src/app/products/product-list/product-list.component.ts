import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Product} from '../../models/Product';
import {Guid} from 'guid-typescript';
import {ProductsService} from '../../Services/products.service';
import {BaseResponse} from '../../models/BaseResponse';
import {MatDialog} from '@angular/material/dialog';
import {DeleteConfirmationDialogComponent} from '../../delete-confirmation-dialog/delete-confirmation-dialog.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.sass']
})
export class ProductListComponent implements OnInit {

  constructor(private route: Router, private productsService: ProductsService, public dialog: MatDialog) {
  }

  productList: Product[] = [];

  ngOnInit(): void {

    this.productsService.getProducts()
      .subscribe((serviceResponse: BaseResponse<Product>) => {
        this.productList = serviceResponse.data;
      });
  }

  openDialog(): any {
    const dialogRef = this.dialog.open(DeleteConfirmationDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
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
