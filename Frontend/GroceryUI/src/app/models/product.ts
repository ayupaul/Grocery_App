export class Product {
  id!: Number;
  description!: String;
  productName!: String;
  category!: String;
  availableQuanity!: String;
  imageLink!: String;
  price!: Number;
  specification!: String;
}
export class Cart{
  cartId!:Number
  userId!:Number
  productId!:Number
  quantity!:Number
}
export class Order{
  orderId!:Number
  userId!:Number
  productId!:Number
  quantity!:Number
  date!:string
}