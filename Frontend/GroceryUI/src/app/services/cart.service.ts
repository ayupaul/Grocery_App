import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CartService {
  Backend:string="http://localhost:5024/api/Cart"
  constructor(private http:HttpClient){}
  //add to cart
  addProductToCart(cartDetails:any):Observable<any>{
    return this.http.post(`${this.Backend}/addToCart`,cartDetails)
  }
  // get all item from cart
  getAllItemFromCart():Observable<any>{
    return this.http.get(`${this.Backend}/getAllItemsInCart`);
  }
  //get items by thier user id
  getItemByUserId(userId:any):Observable<any>{
    return this.http.get(`${this.Backend}/`+ userId)
  }
  //update quantity
  updateQuantity(suserId:any,sproductId:any,squantity:any):Observable<any>{
    const cartData={
      userId:suserId,
      productId:sproductId,
      quantity:squantity
    }
    return this.http.put(`${this.Backend}/updateQuantity`,cartData);
  }
  //check item in cart
  checkItemInCart(suserId:any,sproductId:any):Observable<any>{
    const cartData={
      userId:suserId,
      productId:sproductId
    }
    return this.http.post(`${this.Backend}/checkProductInCart`,cartData);
  }
  //delete all items from cart
  deleteAllItemsFromCart(userId:any){
    return this.http.delete(`${this.Backend}/removeAllItemFromCart/`+userId)
  }
  //update available quantity of product
  updateAvailableQuantityOfProduct(productId:any,availableQuantity:any):Observable<any>{
    const payload={
      id:productId,
      availableQuanity:availableQuantity,
    };
    return this.http.put(`${this.Backend}/updateAvailableQuantity`,payload);
  }
  //remove item from cart
  removeItemFromCart(cartId:any):Observable<any>{
    return this.http.delete(`${this.Backend}/removeFromCart/`+cartId);
  }
}
