export class BaseResponse<T> {
  /**
   * Represents the response data.
   */
  data: T[];
  /**
   * Represents the number of items contained in the data property.
   */
  count: number;

  /**
   * Represents the total available items in the response.
   */
  totalItems: number;

  /**
   * Represents the current index.
   */
  currentIndex: number;

  /**
   * Represents the total available items in the database.
   */
  availableItems: number;
}
