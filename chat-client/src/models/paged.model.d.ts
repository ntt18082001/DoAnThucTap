export interface Paged {
  name?: string;
  page: number;
}

export interface UserPaged {
  id: string;
  fullName: string;
  avatar: string;
  email: string;
}

export interface PagedResponse<T> {
  data: T[];
  page: number;
  size: number;
  totalPages: number;
  totalItems: number;
}