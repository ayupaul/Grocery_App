import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { map } from 'rxjs';
import { NgToastService } from 'ng-angular-popup';
@Component({
  selector: 'app-make-admin',
  templateUrl: './make-admin.component.html',
  styleUrls: ['./make-admin.component.css'],
})
export class MakeAdminComponent implements OnInit {
  listOfUsers: any;
  userArray: any;
  selectedUserArray: any;
  unSelectedUserArray: any;
  constructor(
    private userService: UserService,
    private toast: NgToastService
  ) {}
  ngOnInit(): void {
    this.userService.getAllUsers().subscribe(
      (data) => {
        this.listOfUsers = data;
        this.userArray = this.listOfUsers.map((users: any) => ({
          ...users,
          isSelected: users.role === 'Admin',
        }));
        console.log(this.userArray);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  changeRole() {
    this.selectedUserArray = this.userArray.filter(
      (user: any) => user.isSelected
    );

    this.selectedUserArray.forEach((users: any) => {
      users.role = 'Admin';
      this.userService.changeAdmin(users.id, users.role).subscribe(
        (res) => {
          console.log(res);
          this.toast.success({
            detail: 'Success',
            summary: 'Role Updated Successfully',
            duration: 5000,
          });
        },
        (error) => {
          console.log(error);
          this.toast.error({
            detail: 'Error',
            summary: 'Error Occurs',
            duration: 5000,
          });
        }
      );
    });
    console.log(this.selectedUserArray);
    this.unSelectedUserArray = this.userArray.filter(
      (user: any) => !user.isSelected
    );
    this.unSelectedUserArray.forEach((users: any) => {
      users.role = 'User';
      this.userService.changeAdmin(users.id, users.role).subscribe(
        (res) => {
          console.log(res);
          this.toast.success({
            detail: 'Success',
            summary: 'Role Updated Successfully',
            duration: 5000,
          });
        },
        (error) => {
          console.log(error);
          this.toast.error({
            detail: 'Error',
            summary: 'Error Occurs',
            duration: 5000,
          });
        }
      );
    });
    console.log(this.unSelectedUserArray);
  }
}
