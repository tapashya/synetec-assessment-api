import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeeBonusComponent } from './employee-bonus/employee-bonus.component';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeeBonusService } from './services/employee-bonus.service';

@NgModule({
  declarations: [
    AppComponent,
    EmployeesComponent,
    EmployeeBonusComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    EmployeeBonusService    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
