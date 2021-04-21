import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeBonusComponent } from './employee-bonus/employee-bonus.component';
import { EmployeesComponent } from './employees/employees.component';

const routes: Routes = [
  { path: '', component: EmployeesComponent, pathMatch: 'full' },
  { path: 'employee/:id', component: EmployeeBonusComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
