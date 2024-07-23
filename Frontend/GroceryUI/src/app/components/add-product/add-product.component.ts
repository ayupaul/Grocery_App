import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  addProductForm:FormGroup
  constructor(private formBuilder:FormBuilder,private productService:ProductService,private route:Router){
    this.addProductForm=this.formBuilder.group({
      productName:[''],
      description:[''],
      category:[''],
      availableQuanity:[''],
      imageLink:[''],
      price:[''],
      specification:['']
    })
  }
  onAdd(){
    this.productService.addProduct(this.addProductForm.value).subscribe((res)=>{
      console.log(res)
      this.route.navigateByUrl("/dashboard")
    },(error)=>{
      console.log(error)
      this.route.navigateByUrl("/dashboard")
    })
  }
}
