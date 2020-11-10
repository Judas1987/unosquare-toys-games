import {Injectable} from '@angular/core';
import {Product} from '../models/Product';
import {Guid} from 'guid-typescript';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable, Subscription} from 'rxjs';
import {BaseResponse} from '../models/BaseResponse';

@Injectable({
    providedIn: 'root'
})
export class ProductsService {
    private products: Product[] = [];
    private addedProducts: Product[] = [];

    constructor(private httpClient: HttpClient) {
    }

    getProductById(productId: string): Observable<BaseResponse<Product>> {
        const apiResource = `${environment.productsApiUrl}/products/${productId}`;
        return this.httpClient.get<BaseResponse<Product>>(apiResource);
    }

    getProducts(): Observable<BaseResponse<Product>> {
        const apiResource = `${environment.productsApiUrl}/products`;
        return this.httpClient.get<BaseResponse<Product>>(apiResource);
    }

    addProduct(newProduct: Product): Observable<BaseResponse<Product>> {
        const apiResource = `${environment.productsApiUrl}/products`;

        return this.httpClient.post<BaseResponse<Product>>(apiResource, newProduct, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
    }

    updateProduct(productId: string, existingProduct: Product): Observable<BaseResponse<Product>> {
        const apiResource = `${environment.productsApiUrl}/products/${productId}`;

        return this.httpClient.put<BaseResponse<Product>>(apiResource, existingProduct, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
    }

    deleteProduct(productId): Observable<any> {
        const apiResource = `${environment.productsApiUrl}/products/${productId}`;

        return this.httpClient.delete<BaseResponse<Product>>(apiResource, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
    }
}
