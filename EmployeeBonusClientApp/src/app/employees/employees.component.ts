import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeBonusService } from '../services/employee-bonus.service';
import { Employee } from '../models/employee';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html'
})
export class EmployeesComponent implements OnInit {
  employees$: Observable<Employee[]>;

  constructor(private employeeBonusService: EmployeeBonusService) {
  }
   
  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.employees$ = this.employeeBonusService.getEmployees();
  }

}

