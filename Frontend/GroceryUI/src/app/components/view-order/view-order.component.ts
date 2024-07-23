import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { end } from '@popperjs/core';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-view-order',
  templateUrl: './view-order.component.html',
  styleUrls: ['./view-order.component.css']
})
export class ViewOrderComponent  {
 yearList:any[]=[]
 currentYear:any
 monthList:any[]=["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"]
 monthRequired:any
 yearRequired:any
 productListId:any
 productList:any[]=[]
 constructor(private orderService:OrderService,private productService:ProductService){
  this.currentYear=new Date().getFullYear();
  const startYear=this.currentYear-10;
  const endYear=this.currentYear+10;
  for(let i=startYear;i<=endYear;i++){
    this.yearList.push(i);
  }
 }
 yearSelected(year:any){
  console.log(year.target.value)
  this.yearRequired=year.target.value
 }
 monthSelected(month:any){
  console.log(month.target.value)
  this.monthRequired=month.target.value
 }
search(){
  this.orderService.searchTop(this.yearRequired,this.monthRequired).subscribe((data)=>{
    console.log(data)
    this.productListId=data
    console.log(this.productListId)
    this.productListId.forEach((products:any) => {
      this.productService.getProductById(products).subscribe((data)=>{
        console.log(data)
        this.productList.push(data)
        console.log(this.productList)
      })
    });
  },(error)=>{
    console.log(error);
  }
  )
}
}
