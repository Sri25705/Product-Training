import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = "http://localhost:8009/api/User";

  constructor(private http: HttpClient) {}

  postLogin(data: any) {
    return this.http.post<any>(`${this.api}/login`, data);
  }

  postRegister(data: any) {
  return this.http.post<any>("http://localhost:8009/api/User", data);
}

  getUserById(id: number) {
    return this.http.get<any>(`${this.api}/${id}`);
  }
  updateUser(data: any) {
  return this.http.put<any>("http://localhost:8009/api/User", data);
}
}
