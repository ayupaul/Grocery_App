import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {NgxPaginationModule} from 'ngx-pagination'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AddProductComponent } from './components/add-product/add-product.component';
import { EditProductComponent } from './components/edit-product/edit-product.component';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { CartComponent } from './components/cart/cart.component';
import { FilterPipe } from './shared/filter.pipe';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { MyOrderComponent } from './components/my-order/my-order.component';
import { NgToastModule } from 'ng-angular-popup';
import { MakeAdminComponent } from './components/make-admin/make-admin.component';
import { ViewOrderComponent } from './components/view-order/view-order.component';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    RegisterComponent,
    AddProductComponent,
    EditProductComponent,
    CartComponent,
    FilterPipe,
    ProductDetailsComponent,
    MyOrderComponent,
    MakeAdminComponent,
    ViewOrderComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    NgToastModule,
    NgxPaginationModule
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,
    useClass:TokenInterceptor,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
