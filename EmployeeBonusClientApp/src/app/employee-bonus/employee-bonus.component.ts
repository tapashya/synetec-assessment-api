import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeBonusService } from '../services/employee-bonus.service';
import { Employee } from '../models/employee';
import { Observable } from 'rxjs';
import { CalculateBonusResponse } from '../services/calculate-bonus-response';

@Component({
  selector: 'app-employee-bonus',
  templateUrl: './employee-bonus.component.html'
})
export class EmployeeBonusComponent implements OnInit {
  myForm = new FormGroup({})
  employee$: Observable<Employee>;
  employeeId: number;
  totalBonusPot: number;
  bonusResult$: Observable<CalculateBonusResponse>;

  constructor(private employeeBonusService: EmployeeBonusService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) {
      this.employeeId = this.avRoute.snapshot.params[idParam];
    }
    this.totalBonusPot = 0;
    this.myForm = formBuilder.group(
      {
        totalBonusPot: [null, [Validators.required, Validators.min(0)], Validators.max(2147483647)]
      });    
  }

  ngOnInit() {
    this.loadEmployee();
  }

  loadEmployee(): void {
    this.employee$ = this.employeeBonusService.getEmployee(this.employeeId);
  }                

  calculateBonus(): void {
    if (this.f.totalBonusPot.errors) {
      return;      
    }
    this.bonusResult$ = this.employeeBonusService.calculateBonus(+this.employeeId, +this.myForm.get('totalBonusPot').value); 
  }

  back():void {
    this.router.navigate(['/']);
  }

  get f() { return this.myForm.controls; }
}
