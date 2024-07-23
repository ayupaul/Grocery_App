import { Component, NgZone } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registerForm: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private route: Router,
    private ngZone: NgZone
  ) {
    this.registerForm = this.formBuilder.group(
      {
        fullName: ['', Validators.required, Validators.maxLength(50)],
        email: ['', Validators.required, Validators.email],
        phoneNumber: [
          '',
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(10),
        ],
        password: [
          '',
          Validators.required,
          Validators.minLength(8),
          Validators.pattern(
            /^(?=.*[!@#$%^&*(),.?":{}|<>])(?=.*[0-9])(?=.*[a-zA-Z]).*$/
          ),
        ],
        confirmPassword: ['', Validators.required],
      },
      { validator: this.passwordMatchValidator }
    );
  }
  passwordMatchValidator(control: AbstractControl) {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (password === confirmPassword) {
      return null;
    } else {
      return { passwordMismatchError: true };
    }
  }
  //register
  onRegister() {
    this.userService.register(this.registerForm.value).subscribe(
      (res) => {
        console.log(res);
        this.route.navigateByUrl('/login');
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
