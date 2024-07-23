import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private route: Router,
    private toast: NgToastService
  ) {
    this.loginForm = this.formBuilder.group({
      email: [''],
      password: [''],
    });
  }

  onLogin() {
    return this.userService.login(this.loginForm.value).subscribe(
      (res) => {
        console.log(res);
        this.userService.storeToken(res.token);
        console.log(this.userService.getFullNameFromToken());
        const tokenPayload = this.userService.decodedToken();
        console.log(tokenPayload);
        this.route.navigateByUrl('/dashboard');
        this.toast.success({
          detail: 'Success',
          summary: 'Logged In Successfully',
          duration: 5000,
        });
      },
      (error) => {
        console.log(error);
        this.toast.error({
          detail: 'Error Message',
          summary: 'Login Fails!!',
          duration: 5000,
        });
      }
    );
  }
}
