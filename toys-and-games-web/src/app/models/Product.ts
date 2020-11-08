export class Product {

  name: string;
  price: number;
  company: string;
  ageRestriction: number;
  description: string;

  /**
   * Product class constructor.
   * @param name Represents the product name.
   * @param price Represents the product price.
   * @param company Represents the product company.
   * @param ageRestriction Represents the age restriction.
   * @param description Represents the description.
   */
  constructor(name: string, price: number, company: string, ageRestriction: number, description: string) {
    this.name = name;
    this.price = price;
    this.company = company;
    this.ageRestriction = ageRestriction;
    this.description = description;
  }
}
