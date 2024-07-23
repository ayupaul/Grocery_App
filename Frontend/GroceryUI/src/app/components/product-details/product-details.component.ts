import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Cart, Product } from 'src/app/models/product';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';
import { ReviewService } from 'src/app/services/review.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent implements OnInit {
  productData: any;
  getId: any;
  quantity: Number = 0;
  cartDetails: Cart;
  isInCart: Boolean = false;
  isSignedIn: any;
  userIdFromToken: Number = 0;
  review: any;
  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private userService: UserService,
    private cartService: CartService,
    private toast: NgToastService,
    private reviewService: ReviewService
  ) {
    this.cartDetails = {
      productId: 0,
      cartId: 0,
      userId: 0,
      quantity: 0,
    };
    this.getId = this.route.snapshot.paramMap.get('id');
    this.productService.getProductById(this.getId).subscribe(
      (data) => {
        this.productData = data;
        console.log(data);
      },
      (error) => {
        console.log(error);
      }
    );
    this.reviewService.getReview(this.getId).subscribe(
      (res) => {
        console.log(res);
        this.review = res;
      },
      (error) => {
        console.log(error);
      }
    );
    this.userIdFromToken = this.userService.getUserIdFromToken();
    this.cartService
      .checkItemInCart(this.userIdFromToken, this.getId)
      .subscribe((res) => {
        console.log(res);
        this.isInCart = true;
      });
    this.isSignedIn = this.userService.isLoggedIn();
  }
  ngOnInit(): void {}
  addToCart() {
    this.cartDetails.productId = this.getId;

    console.log(this.userIdFromToken);
    this.cartDetails.userId = this.userIdFromToken;
    this.cartDetails.quantity = this.quantity;
    this.cartService.addProductToCart(this.cartDetails).subscribe(
      (res) => {
        console.log(res);
      },
      (error) => {
        console.log(error);
      }
    );
    console.log('Add to cart executed successfully');
    this.toast.success({
      detail: 'Success',
      summary: 'Added To Cart Successfully',
      duration: 5000,
    });
    location.reload();
  }
}
