import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Cart } from 'src/app/models/product';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [],
})
export class DashboardComponent implements OnInit {
  isSignedIn: any;
  public fullName: string = '';
  public roleName: any;
  searchForm: string = '';
  searchedData: any;
  productsList: Array<any>;
  checkRoleName: any;
  checkedRoleName: Boolean;
  userId: any;
  searchKey: string = '';
  categoryKey: string = '';
  currentPageNumber: number = 1;
  constructor(
    private userService: UserService,
    private route: Router,
    private productService: ProductService,
    private formBuilder: FormBuilder,
    private cartService: CartService,
    private toast: NgToastService
  ) {
    this.productsList = new Array<any>();
    this.userId = this.userService.getUserIdFromToken();
    this.isSignedIn = this.userService.isLoggedIn();

    this.checkRoleName = 'Admin';
    if (this.isSignedIn == true) {
      this.fullName = this.userService.getFullNameFromToken();
    }
    // console.log(this.fullName)
    this.roleName = this.userService.getRoleFromToken();
    console.log(this.roleName);
    if (this.roleName == this.checkRoleName) {
      this.checkedRoleName = true;
    } else {
      this.checkedRoleName = false;
    }
    console.log(this.checkedRoleName);
    console.log(typeof this.roleName);
    console.log(this.roleName);

    this.productService.getAllProducts().subscribe(
      (res) => {
        this.productsList = res;
        console.log(this.productsList);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  public ngOnInit(): void {
    // this.userStoreService.fullNameValue.subscribe((name) => {
    //   console.log(name);
    // });
    // this.userStoreService.getFullNameFromStore().subscribe(
    //   (val) => {
    //     console.log(val);
    //   },
    //   (error) => {
    //     console.log(error);
    //   }
    // );
  }
  //logout
  logout() {
    this.userService.SignOut();
    this.isSignedIn = false;
    this.toast.success({
      detail: 'Success',
      summary: 'Logout Successfully!!!',
      duration: 5000,
    });
    this.route.navigateByUrl('/dashboard');
    location.reload();
  }
  //search for products by thier name
  Search(event: any) {
    this.searchForm = (event.target as HTMLInputElement).value;
    this.searchKey = this.searchForm;
  }
  //update product
  updateProduct(id: any) {
    this.route.navigateByUrl(`/editProduct/${id}`);
    // this.toast.success({detail:"Success",summary:"Updated Product Successfully",duration:5000});
  }
  //delete product
  deleteProduct(id: any) {
    if (window.confirm('Are You sure you want to delete this product')) {
      this.productService.deleteProduct(id).subscribe(
        (res) => {
          console.log(res);
          this.route.navigateByUrl('/dashboard');
        },
        (error) => {
          console.log(error);
        }
      );
    }
    this.toast.success({
      detail: 'Success',
      summary: 'Deleted Product Successfully',
      duration: 5000,
    });
  }

  //fliter category
  filterCategory(category: string) {
    this.categoryKey = category;
  }
}
