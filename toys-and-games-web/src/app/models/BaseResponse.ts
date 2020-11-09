export class BaseResponse<T> {
  /**
   * Represents the response data.
   */
  data: T[];
  count: number;
  totalItems: number;
  currentIndex: number;
  availableItems: number;
}
