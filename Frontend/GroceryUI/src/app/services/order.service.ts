import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  Backend:string="http://localhost:5024/api/Order"
  constructor(private http:HttpClient) { }
  //place order
  placeOrder(suserId:any,sproductId:any,squantity:any,dateOfOrder:any){
    const payload={
      userId:suserId,
      productId:sproductId,
      quantity:squantity,
      orderDate:dateOfOrder
    }
    return this.http.post(`${this.Backend}/placeOrder`,payload);
  }
  //get your order details
  getOrderDetails(userId:any):Observable<any>{
    return this.http.get(`${this.Backend}/`+ userId);
  }
  //search top 5 orders
  searchTop(year:any,month:string):Observable<any>{
    return this.http.get(`${this.Backend}/searchTop5Orders?month=${month}&year=${year}`)
  }
}
