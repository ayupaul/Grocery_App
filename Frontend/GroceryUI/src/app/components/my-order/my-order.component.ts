import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Order } from 'src/app/models/product';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';
import { ReviewService } from 'src/app/services/review.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-my-order',
  templateUrl: './my-order.component.html',
  styleUrls: ['./my-order.component.css'],
})
export class MyOrderComponent implements OnInit {
  getId: any;
  listOrderItems: any;
  reviewForm: FormGroup;
  constructor(
    private orderService: OrderService,
    private route: ActivatedRoute,
    private productService: ProductService,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private reviewService: ReviewService,
    private toast: NgToastService
  ) {
    this.getId = this.route.snapshot.paramMap.get('userId');
    this.reviewForm = this.formBuilder.group({
      ratingControl: 0,
      review: '',
    });
  }

  ngOnInit() {
    this.orderService.getOrderDetails(this.getId).subscribe(
      (data) => {
        this.listOrderItems = data;
        const numberOfItems = this.listOrderItems.length;
        let counter = 0;
        this.listOrderItems.forEach((order: Order, index: number) => {
          this.productService.getProductById(order.productId).subscribe(
            (data) => {
              this.listOrderItems[index].productDetails = data;
              counter++;
              if (counter == numberOfItems) {
                console.log(this.listOrderItems);
              }
            },
            (error) => {
              console.log(error);
            }
          );
        });
      },
      (error) => {
        console.log(error);
      }
    );
  }
  calculateGrandTotalPrice(): number {
    let totalPrice = 0;
    for (const orderItem of this.listOrderItems) {
      totalPrice += orderItem.productDetails.price * orderItem.quantity;
    }
    return totalPrice;
  }
  submitReviewForm(productId: any) {
    const rating = this.reviewForm.get('ratingControl')?.value;
    const review = this.reviewForm.get('review')?.value;
    const userName = this.userService.getFullNameFromToken();
    const date = new Date();
    const dateOfReview = date.toDateString();
    this.reviewService
      .addReview(rating, review, userName, productId, dateOfReview)
      .subscribe(
        (res) => {
          console.log(res);
          this.toast.success({
            detail: 'Success',
            summary: 'Review Added',
            duration: 5000,
          });
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
