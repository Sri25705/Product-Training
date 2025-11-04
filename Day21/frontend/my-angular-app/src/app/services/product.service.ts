import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private api = "http://localhost:8009/api/Product";

  constructor(private http: HttpClient) {}

  getProducts(): Observable<any> {
    return this.http.get<any>(`${this.api}`);
  }

  addProduct(data: any): Observable<any> {
    return this.http.post(`${this.api}`, data);
  }
}


