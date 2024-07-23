import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  Backend:string="http://localhost:5024/api/Review"
  constructor(private http:HttpClient) { }
  //add review
  addReview(srating:any,sreview:any,suserName:any,sproductId:any,dateOfReview:any):Observable<any>{
    const payload={
     rating:srating,
     review:sreview,
     userName:suserName,
     productId:sproductId,
     date:dateOfReview,
    }
    return this.http.post(`${this.Backend}/addReview`,payload);
  }
  //get review by product id
  getReview(id:any){
    return this.http.get(`${this.Backend}/`+id);
  }
}
