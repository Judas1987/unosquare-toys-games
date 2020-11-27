import {Injectable} from '@angular/core';
import {Product} from '../models/Product';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs';
import {BaseResponse} from '../models/BaseResponse';

@Injectable({
    providedIn: 'root'
})
export class ProductsService {

    /**
     * Class constructor
     * @param httpClient Represents the HTTP client instance.
     */
    constructor(private httpClient: HttpClient) {
    }

    /**
     * This function gets a product by its identifier.
     * @param productId Represents the product identifier.
     */
    getProductById(productId: string): Observable<BaseResponse<Product>> {
        const apiResource = `${environment.productsApiUrl}/products/${productId}`;
        return this.httpClient.get<BaseResponse<Product>>(apiResource);
    }

    /**
     * This function gets all the existing products from the API.
     */
    getProducts(): Observable<BaseResponse<Product>> {
        const apiResource = `${environment.productsApiUrl}/products`;
        return this.httpClient.get<BaseResponse<Product>>(apiResource);
    }

    /**
     * This function sends a POST request to the products API in order to create a new entry in the DB.
     * @param newProduct Represents the product to be added.
     */
    addProduct(newProduct: Product): Observable<BaseResponse<Product>> {
        const apiResource = `${environment.productsApiUrl}/products`;

        return this.httpClient.post<BaseResponse<Product>>(apiResource, newProduct, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
    }

    /**
     * This function sends a PUT request to the product API in order to update an existing product.
     * @param productId Represents the product id to be used to update a specific product.
     * @param existingProduct Represents the instance of the existing product.
     */
    updateProduct(productId: string, existingProduct: Product): Observable<BaseResponse<Product>> {
        const apiResource = `${environment.productsApiUrl}/products/${productId}`;

        return this.httpClient.put<BaseResponse<Product>>(apiResource, existingProduct, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
    }

    /**
     * This function sends a DELETE request to a given resource in the product API.
     * @param productId Represents the product identifier.
     */
    deleteProduct(productId): Observable<any> {
        const apiResource = `${environment.productsApiUrl}/products/${productId}`;

        return this.httpClient.delete<BaseResponse<Product>>(apiResource, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
    }
}
