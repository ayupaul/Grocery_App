import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
})
export class EditProductComponent {
  addProductForm: FormGroup;
  getId: any;
  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.getId = this.route.snapshot.paramMap.get('id');
    console.log(this.getId);
    this.addProductForm = this.formBuilder.group({
      productName: [''],
      description: [''],
      category: [''],
      availableQuanity: [''],
      imageLink: [''],
      price: [''],
      specification: [''],
    });
    this.productService.getProductById(this.getId).subscribe((data) => {
      console.log(data);
      this.addProductForm.setValue({
        productName: data['productName'],
        description: data['description'],
        category: data['category'],
        availableQuanity: data['availableQuanity'],
        imageLink: data['imageLink'],
        price: data['price'],
        specification: data['specification'],
      });
    });
  }
  onEdit() {
    this.productService
      .updateProduct(this.getId, this.addProductForm.value)
      .subscribe(
        (res) => {
          console.log(res);
          this.router.navigateByUrl('/dashboard');
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
