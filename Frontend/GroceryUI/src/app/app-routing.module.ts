import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { EditProductComponent } from './components/edit-product/edit-product.component';
import { AuthGuard } from './guards/auth.guard';
import { ActionsGuard } from './guards/actions.guard';
import { CartComponent } from './components/cart/cart.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { MyOrderComponent } from './components/my-order/my-order.component';
import { MakeAdminComponent } from './components/make-admin/make-admin.component';
import { ViewOrderComponent } from './components/view-order/view-order.component';


const routes: Routes = [
  {path:'',component:DashboardComponent},
  {path:'dashboard',component:DashboardComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'addProduct',component:AddProductComponent,canActivate:[AuthGuard]},
  {path:'editProduct/:id',component:EditProductComponent,canActivate:[AuthGuard]},
  {path:'cart/:userId',component:CartComponent,canActivate:[ActionsGuard]},
  {path:'productDetails/:id',component:ProductDetailsComponent},
  {path:'orderDetails/:userId',component:MyOrderComponent,canActivate:[ActionsGuard]},
  {path:'admin',component:MakeAdminComponent,canActivate:[AuthGuard]},
  {path:'viewOrder',component:ViewOrderComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
