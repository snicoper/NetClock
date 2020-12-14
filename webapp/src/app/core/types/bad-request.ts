export interface BadRequest {
  errors: string[];
  type: string;
  title: string;
  status: number;
}
