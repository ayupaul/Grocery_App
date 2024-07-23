import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Cart, Product } from 'src/app/models/product';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent {
  getUserId: any;
  listOfCartItems: any;
  constructor(
    private cartService: CartService,
    private productService: ProductService,
    private route: ActivatedRoute,
    private userService: UserService,
    private orderService: OrderService,
    private toast: NgToastService
  ) {
    this.getUserId = this.route.snapshot.paramMap.get('userId');
    this.cartService.getItemByUserId(this.getUserId).subscribe(
      (data) => {
        this.listOfCartItems = data;
        const numberOfItems = this.listOfCartItems.length;
        let counter = 0;
        this.listOfCartItems.forEach((items: Cart, index: number) => {
          this.productService.getProductById(items.productId).subscribe(
            (data) => {
              this.listOfCartItems[index].productDetails = data;
              counter++;
              if (counter === numberOfItems) {
                // All product details have been fetched
                console.log(this.listOfCartItems);
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
  decreaseQuantity(cartItem: any, id: any) {
    if (cartItem.quantity > 1) {
      cartItem.quantity--;
    }
    this.updateBackend(cartItem.quantity, id);
  }

  increaseQuantity(cartItem: any, id: any) {
    cartItem.quantity++;
    this.updateBackend(cartItem.quantity, id);
  }

  updateBackend(quantity: any, id: any) {
    const userId = this.userService.getUserIdFromToken();
    this.cartService.updateQuantity(userId, id, quantity).subscribe(
      (res) => {
        console.log(res);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  calculateGrandTotalPrice(): number {
    let totalPrice = 0;
    for (const cartItem of this.listOfCartItems) {
      totalPrice += cartItem.productDetails.price * cartItem.quantity;
    }
    return totalPrice;
  }
  placeOrder() {
    const userId = this.listOfCartItems[0].userId;
    for (const cartItem of this.listOfCartItems) {
      const productId = cartItem.productDetails.id;
      const quantity = cartItem.quantity;
      const updatedQuanity =
        cartItem.productDetails.availableQuanity - cartItem.quantity;
      const dateOfOrder = new Date().toDateString();
      this.cartService
        .updateAvailableQuantityOfProduct(productId, updatedQuanity)
        .subscribe(
          (res) => {
            console.log(res);
          },
          (error) => {
            console.log(error);
          }
        );
      this.orderService
        .placeOrder(userId, productId, quantity, dateOfOrder)
        .subscribe(
          (res) => {
            console.log(res);
          },
          (error) => {
            console.log(error);
            this.toast.error({
              detail: 'Error',
              summary: 'Unable to place order',
              duration: 5000,
            });
          }
        );
    }
    this.toast.success({
      detail: 'Success',
      summary: 'Order Placed',
      duration: 5000,
    });
    this.cartService.deleteAllItemsFromCart(userId).subscribe(
      (res) => {
        console.log(res);
      },
      (error) => {
        console.log(error);
      }
    );
    location.reload();
  }
  // remove item from cart
  onRemove(cartId: any) {
    this.cartService.removeItemFromCart(cartId).subscribe(
      (res) => {
        console.log(res);
        this.toast.success({
          detail: 'Success',
          summary: 'Item removed successfully!!!',
          duration: 5000,
        });
      },
      (error) => {
        console.log(error);
        this.toast.error({
          detail: 'Error',
          summary: 'Unable to remove product',
          duration: 5000,
        });
      }
    );
    location.reload();
  }
}
