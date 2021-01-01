import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from './guards/authorize.guard';
import { ChangePassword } from './models/change-password';
import { AddQuestionComponent } from './pages/add-question/add-question.component';
import { AdminComponent } from './pages/admin/admin.component';
import { ChangePasswordComponent } from './pages/change-password/change-password.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
const routes: Routes = [{
  path: '',
  component: HomeComponent
},
{
  path: 'AddQuestion',
  component: AddQuestionComponent,
  canActivate: [AuthorizeGuard]
},
{
  path: 'Admin',
  component: AdminComponent,
  canActivate: [AuthorizeGuard]
},
{
  path: 'Register',
  component: RegisterComponent,
  canActivate: [AuthorizeGuard]
},
{
  path: 'Login',
  component: LoginComponent
},
{
  path: 'ChangePassword/:userId',
  component: ChangePasswordComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
