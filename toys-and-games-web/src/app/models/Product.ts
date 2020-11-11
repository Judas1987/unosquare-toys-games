import {Guid} from 'guid-typescript';

export class Product {

  /**
   * Represents the product name.
   */
  name: string;

  /**
   * Represents the price.
   */
  price: number;

  /**
   * Represents the company name.
   */
  company: string;

  /**
   * Represents the age restriction.
   */
  ageRestriction: number;

  /**
   * Represents the product description.
   */
  description: string;

  /**
   * Represents the product id.
   */
  productId: Guid;

  /**
   * Product class constructor.
   * @param productId Represents the product id.
   * @param name Represents the product name.
   * @param price Represents the product price.
   * @param company Represents the product company.
   * @param ageRestriction Represents the age restriction.
   * @param description Represents the description.
   */
  constructor(productId: Guid, name: string, price: number, company: string, ageRestriction: number, description: string) {
    this.name = name;
    this.price = price;
    this.company = company;
    this.ageRestriction = ageRestriction;
    this.description = description;
    this.productId = productId;
  }
}
