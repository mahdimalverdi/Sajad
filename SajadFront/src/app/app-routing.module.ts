import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddQuestionComponent } from './pages/add-question/add-question.component';
import { AdminComponent } from './pages/admin/admin.component';
import { HomeComponent } from './pages/home/home.component';
const routes: Routes = [{
  path: '',
  component: HomeComponent
},
{
  path: 'AddQuestion',
  component: AddQuestionComponent
},
{
  path: 'Admin',
  component: AdminComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
