import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private api = "http://localhost:8009/api/Order";

  constructor(private http: HttpClient) {}

  addOrderItem(item: any): Observable<any> {
    return this.http.post<any>(`${this.api}/add-item`, item);
  }

  getOrderDetails(userId: number): Observable<any> {
    return this.http.get<any>(`${this.api}/details/${userId}`);
  }

  getLatestOrderId(userId: number) {
  return this.http.get<number>(`${this.api}/latest/${userId}`);
}
addOrder(data: any) {
  return this.http.post<number>(`${this.api}`, data);
}
getAllItems(userId: number) {
  return this.http.get<any[]>(`${this.api}/all-items/${userId}`);
}
deleteOrder(orderId: number) {
  return this.http.delete(`http://localhost:8009/api/order/${orderId}`);
}
}

