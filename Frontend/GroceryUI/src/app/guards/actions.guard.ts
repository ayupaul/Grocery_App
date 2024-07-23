import { Injectable } from '@angular/core';
import { CanActivate, Router} from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root'
})
export class ActionsGuard implements CanActivate {
  constructor(private userService:UserService,private router:Router){}
  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    if (this.userService.isLoggedIn()===true) {
      return true;
    } else {
      this.router.navigateByUrl("/dashboard");
      return false;
    }
  }
  
}
