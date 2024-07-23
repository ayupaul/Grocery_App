import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  Backend:string="http://localhost:5024/api/Product"
  constructor(private http:HttpClient) { }
  //get product on search
  getProductByName(ProductName:string):Observable<any>{
    const params=new HttpParams().set('ProductName',ProductName)
    return this.http.get(`${this.Backend}/getProductOnSearch`,{params})
  }
  //get all products
  getAllProducts():Observable<any>{
    return this.http.get(`${this.Backend}/getAllProduct`)
  }
  //add product
  addProduct(productDetails:any):Observable<any>{
    return this.http.post(`${this.Backend}/addProduct`,productDetails)
  }
  //get product by id
  getProductById(id:any):Observable<any>{
    return this.http.get(`${this.Backend}/`+ id)
  }
  //update product
  updateProduct(id:any,productDetails:any):Observable<any>{
    return this.http.put(`${this.Backend}/${id}`,productDetails)
  }
  //delete product
  deleteProduct(id:any):Observable<any>{
    return this.http.delete(`${this.Backend}/deleteProduct/${id}`)
  }
}
