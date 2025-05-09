export interface IPaginated<T> {
  data: T[];
  totalPages: number;
  currentPage: number;
  totalItems: number;
}