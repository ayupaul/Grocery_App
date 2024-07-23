import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  Backend: string = 'http://localhost:5024/api/User';
  public userPayload: any;
  constructor(private http: HttpClient, private route: Router) {
    this.userPayload = this.decodedToken();
  }
  //register
  register(userDetails: any): Observable<any> {
    return this.http.post(`${this.Backend}/register`, userDetails);
  }
  //login
  login(userDetails: any): Observable<any> {
    return this.http.post(`${this.Backend}/login`, userDetails);
  }
  //get all users
  getAllUsers():Observable<any>{
    return this.http.get(`${this.Backend}/getAllUser`)
  }
  //change admin
  changeAdmin(userId:any,sRole:any):Observable<any>{
    const payload={
      id:userId,
      role:sRole
    }
    return this.http.put(`${this.Backend}/updateAdmin`,payload);
  }
  //store token
  storeToken(tokenValue: string) {
    localStorage.setItem('token', tokenValue);
  }
  //get token
  getToken() {
    return localStorage.getItem('token');
  }
  //is logged in
  isLoggedIn(): Boolean {
    return !!localStorage.getItem('token');
  }
  //signout
  SignOut() {
    localStorage.clear();
    this.userPayload = undefined;
  }
  //decode token
  decodedToken() {
    const jwtHelper = new JwtHelperService();
    const token = this.getToken() ?? '';
    return jwtHelper.decodeToken(token);
  }
  //get full name from token
  getFullNameFromToken(): string {
    if (this.userPayload) {
      return this.userPayload.name;
    } else {
      this.userPayload = this.decodedToken();
      return this.userPayload.name;
    }
  }
  //get role from token
  getRoleFromToken() {
    if (this.userPayload) {
      return this.userPayload.role;
    }
  }
  getUserIdFromToken(){
    if(this.userPayload){
      console.log(this.userPayload)
      return this.userPayload.UserId
    }
  }
}
